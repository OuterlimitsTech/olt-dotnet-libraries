using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public static partial class OltServiceCollectionExtensions
    {

        /// <summary>
        /// Build Default AspNetCore Service and configures Dependency Injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="action">Invoked after initialized</param>
        /// <returns></returns>
        public static IServiceCollection AddOltAspNetCore<TSettings>(this IServiceCollection services, TSettings settings, Action<IMvcBuilder> action = null) where TSettings : OltAspNetAppSettings
        {
            return services.AddOltAspNetCore(settings, new List<Assembly>(), action);
        }

        /// <summary>
        /// Build Default AspNetCore Service and configures Dependency Injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="baseAssembly">Assembly to include in scan for interfaces</param>
        /// <param name="action">Invoked after initialized</param>
        /// <returns></returns>
        public static IServiceCollection AddOltAspNetCore<TSettings>(this IServiceCollection services, TSettings settings, Assembly baseAssembly, Action<IMvcBuilder> action = null) where TSettings : OltAspNetAppSettings
        {
            return services.AddOltAspNetCore(settings, new List<Assembly>() { baseAssembly }, action);
        }

        /// <summary>
        /// Build Default AspNetCore Service and configures Dependency Injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="baseAssemblies">List of assemblies to include in scan for interfaces</param>
        /// <param name="action">Invoked after initialized</param>
        /// <returns></returns>
        public static IServiceCollection AddOltAspNetCore<TSettings>(this IServiceCollection services, TSettings settings, List<Assembly> baseAssemblies, Action<IMvcBuilder> action = null)
            where TSettings : IOltOptionsAspNet
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            baseAssemblies.Add(Assembly.GetEntryAssembly());
            baseAssemblies.Add(Assembly.GetExecutingAssembly());

            services
                .AddCors(baseAssemblies)
                .AddApiVersioning(new OltOptionsApiVersion())
                .AddOltInjection(baseAssemblies)
                .AddSingleton<IOltHostService, OltHostAspNetCoreService>()
                .AddScoped<IOltIdentity, OltIdentityAspNetCore>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>())
                .AddHttpContextAccessor();

            action?.Invoke(services.AddControllers());

            return services;
        }



        /// <summary>
        /// Adds API version as query string and defaults to 1.0 if not present
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltOptionsApiVersion"/></param>
        /// <param name="setupVersioningAction"><seealso cref="ApiVersioningOptions"/></param>
        /// <param name="setupExplorerAction"><seealso cref="ApiExplorerOptions"/></param>
        /// <returns><seealso cref="IServiceCollection"/></returns>
        public static IServiceCollection AddApiVersioning<TOptions>(this IServiceCollection services, TOptions options, Action<ApiVersioningOptions> setupVersioningAction = null, Action<ApiExplorerOptions> setupExplorerAction = null)
            where TOptions : IOltOptionsApiVersion
        {
            if (options.Enabled)
            {
                return services
                    .AddApiVersioning(opt =>
                    {
                        opt.ApiVersionReader = new QueryStringApiVersionReader(options.ApiQueryParameterName);
                        opt.AssumeDefaultVersionWhenUnspecified = options.AssumeDefaultVersionWhenUnspecified;
                        opt.DefaultApiVersion = new ApiVersion(1, 0);
                        opt.ReportApiVersions = true;
                        setupVersioningAction?.Invoke(opt);
                    })
                    .AddVersionedApiExplorer(
                        opt =>
                        {
                            //The format of the version added to the route URL  
                            opt.GroupNameFormat = "'v'VVV";
                            //Tells swagger to replace the version in the controller route  
                            opt.SubstituteApiVersionInUrl = true;
                            setupExplorerAction?.Invoke(opt);
                        });
            }

            return services;
        }



        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public static class OltServiceCollectionExtensions
    {

        /// <summary>
        /// Configures Default Assembly Scan and adds memory cache
        /// Adds IOltMemoryCache, IOltConfigManager, IOltLogService as Singletons
        /// Adds IOltDbAuditUser to resolve to IOltIdentity as scoped
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection AddOltDefault(this IServiceCollection services, Action action)
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            services                
                .OltScan()
                .AddSingleton<IOltMemoryCache, OltMemoryCache>()
                .AddSingleton<IOltConfigManager, OltConfigManager>()
                .AddSingleton<IOltLogService, OltLogService>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());

            action.Invoke();

            return services;
        }


        /// <summary>
        /// Adds Memory Cache
        /// </summary>
        /// <param name="services"></param>
        /// <param name="expirationMinutes"></param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection AddOltAddMemoryCache(this IServiceCollection services, int expirationMinutes = 30)
        {
            if (expirationMinutes <= 0)
            {
                throw new ArgumentNullException(nameof(expirationMinutes));
            }

            return services.AddMemoryCache(o => new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(expirationMinutes)));
        }

        /// <summary>
        /// Scans OLT interfaces to associated DI by name and other default classes for OLT interfaces that do not conform to naming convention
        /// IOltInjectable
        /// IOltInjectableScoped
        /// IOltInjectableTransient
        /// IOltInjectableSingleton
        /// </summary>
        /// <param name="services"></param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static List<Assembly> OltScanAssemblies(this IServiceCollection services)
        {
            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetEntryAssembly(),
                Assembly.GetExecutingAssembly()
            };

            return assembliesToScan.GetAllReferencedAssemblies();
        }

        /// <summary>
        /// Scans OLT interfaces to associated DI by name and other default classes for OLT interfaces that do not conform to naming convention
        /// IOltInjectable
        /// IOltInjectableScoped
        /// IOltInjectableTransient
        /// IOltInjectableSingleton
        /// </summary>
        /// <param name="services"></param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection OltScan(this IServiceCollection services)
        {
            return OltScan(services, services.OltScanAssemblies());
        }

        /// <summary>
        /// Scans OLT interfaces to associated DI by name and other default classes for OLT interfaces that do not conform to naming convention
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembliesToScan">List of Assemblies To Scan for interfaces</param>
        /// <returns></returns>
        public static IServiceCollection OltScan(this IServiceCollection services, List<Assembly> assembliesToScan)
        {

            services.Scan(sc =>
                sc.FromAssemblies(assembliesToScan)
                    //.AddClasses(classes => classes.AssignableTo<IOltInjectable>())
                    //.AsImplementedInterfaces()
                    //.WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo<IOltInjectableScoped>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo<IOltInjectableTransient>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    .AddClasses(classes => classes.AssignableTo<IOltInjectableSingleton>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
            );




            return services;
        }


    }
}
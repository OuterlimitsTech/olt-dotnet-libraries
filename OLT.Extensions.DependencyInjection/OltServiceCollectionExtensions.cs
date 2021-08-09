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
        /// Configures Default Assembly Scan along with the mentioned interfaces
        /// </summary>
        /// <remarks>
        /// Adds <see cref="IOltConfigManager"/> and <see cref="IOltLogService"/> as singletons
        /// </remarks>
        /// Adds <see cref="IOltDbAuditUser"/> to resolve to <see cref="IOltIdentity"/> as scoped
        /// <remarks>
        /// </remarks>
        /// <param name="services"></param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection AddOltDefault(this IServiceCollection services)
        {
            return AddOltDefault(services, new List<Assembly>());
        }


        /// <summary>
        /// Configures Default Assembly Scan along with the mentioned interfaces
        /// </summary>
        /// <remarks>
        /// Adds <see cref="IOltConfigManager"/> and <see cref="IOltLogService"/> as singletons
        /// </remarks>
        /// Adds <see cref="IOltDbAuditUser"/> to resolve to <see cref="IOltIdentity"/> as scoped
        /// <remarks>
        /// </remarks>
        /// <param name="services"></param>
        /// <param name="includeAssembliesScan">List of assemblies to start scan for interfaces</param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection AddOltDefault(this IServiceCollection services, List<Assembly> includeAssembliesScan)
        {
            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetEntryAssembly(),
                Assembly.GetExecutingAssembly()
            };

            assembliesToScan.AddRange(includeAssembliesScan);

            return services
                .OltScan(assembliesToScan.GetAllReferencedAssemblies())
                .AddSingleton<IOltConfigManager, OltConfigManager>()
                .AddSingleton<IOltLogService, OltLogService>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());
        }

        /// <summary>
        /// Adds Memory Cache
        /// </summary>
        /// <remarks>
        /// Registers <see cref="IOltMemoryCache"/> as a singleton
        /// </remarks>
        /// <param name="services"></param>
        /// <param name="expirationMinutes"></param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection AddOltAddMemoryCache(this IServiceCollection services, int expirationMinutes = 30)
        {
            if (expirationMinutes <= 0)
            {
                throw new ArgumentNullException(nameof(expirationMinutes));
            }


            return services
                .AddSingleton<IOltMemoryCache, OltMemoryCache>()
                .AddMemoryCache(o => new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(expirationMinutes)));
        }

        ///// <summary>
        ///// Builds a collection of assemblies referenced by the root assemblies
        ///// </summary>
        ///// <param name="services"></param>
        ///// <returns><param typeof="IServiceCollection"></param></returns>
        //public static List<Assembly> OltScanAssemblies(this IServiceCollection services)
        //{
        //    var assembliesToScan = new List<Assembly>
        //    {
        //        Assembly.GetEntryAssembly(),
        //        Assembly.GetExecutingAssembly()
        //    };

        //    return assembliesToScan.GetAllReferencedAssemblies();
        //}

        ///// <summary>
        ///// Scans <see cref="IOltInjectableScoped"/>, <see cref="IOltInjectableSingleton"/>, and <see cref="IOltInjectableTransient"/> to associated DI by name 
        ///// </summary>
        ///// <param name="services"></param>
        ///// <returns><param typeof="IServiceCollection"></param></returns>
        //public static IServiceCollection OltScan(this IServiceCollection services)
        //{
        //    return OltScan(services, services.OltScanAssemblies());
        //}

        /// <summary>
        /// SScans <see cref="IOltInjectableScoped"/>, <see cref="IOltInjectableSingleton"/>, and <see cref="IOltInjectableTransient"/> to associated DI by name 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembliesToScan">List of Assemblies To Scan for interfaces</param>
        /// <returns></returns>
        public static IServiceCollection OltScan(this IServiceCollection services, List<Assembly> assembliesToScan)
        {
            return services.Scan(sc =>
                sc.FromAssemblies(assembliesToScan)
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
        }


    }
}
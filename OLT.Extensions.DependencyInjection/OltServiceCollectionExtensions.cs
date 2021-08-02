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
        /// <param name="options"></param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection AddOltDefault(this IServiceCollection services, IOltInjectionOptions options)
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            services
                .AddOltAddMemoryCache(options.CacheExpirationMinutes)
                .OltScan()
                .AddSingleton<IOltMemoryCache, OltMemoryCache>()
                .AddSingleton<IOltConfigManager, OltConfigManager>()
                .AddSingleton<IOltLogService, OltLogService>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());

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

            //if (assembliesToScan.Any(p => p.FullName != Assembly.GetCallingAssembly().FullName))
            //{
            //    assembliesToScan.Add(Assembly.GetCallingAssembly());
            //}

            //var referencedAssemblies = new List<Assembly>();
            //assembliesToScan.ForEach(assembly =>
            //{
            //    referencedAssemblies.AddRange(assembly.GetReferencedAssemblies().Select(Assembly.Load));
            //});

            //AppDomain.CurrentDomain
            //    .GetAssemblies()
            //    .ToList()
            //    .ForEach(assembly =>
            //    {
            //        referencedAssemblies.Add(assembly);
            //    });


            //referencedAssemblies
            //    .GroupBy(g => g.FullName)
            //    .Select(s => s.Key)
            //    .OrderBy(o => o)
            //    .ToList()
            //    .ForEach(name =>
            //    {
            //        var assembly = assembliesToScan.FirstOrDefault(p => string.Equals(p.FullName, name, StringComparison.OrdinalIgnoreCase));
            //        if (assembly == null)
            //        {
            //            assembliesToScan.Add(referencedAssemblies.FirstOrDefault(p => p.FullName == name));
            //        }
            //    });


            //return assembliesToScan;
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
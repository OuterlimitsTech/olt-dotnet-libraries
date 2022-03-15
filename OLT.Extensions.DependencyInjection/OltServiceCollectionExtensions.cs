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
        /// Adds <see cref="IOltDbAuditUser"/> to resolve to <see cref="IOltIdentity"/> as scoped
        /// </remarks>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection AddOltInjection(this IServiceCollection services)
        {
            return AddOltInjection(services, new List<Assembly>());
        }


        /// <summary>
        /// Configures Default Assembly Scan along with the mentioned interfaces
        /// </summary>
        /// <remarks>
        /// Adds <see cref="IOltDbAuditUser"/> to resolve to <see cref="IOltIdentity"/> as scoped
        /// </remarks>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="baseAssembly">Assembly to include in scan for interfaces</param>
        /// <returns><param typeof="IServiceCollection"></param></returns>
        public static IServiceCollection AddOltInjection(this IServiceCollection services, Assembly baseAssembly)
        {
            return AddOltInjection(services, new List<Assembly>() { baseAssembly });
        }


        /// <summary>
        /// Scans <see cref="IOltInjectableScoped"/>, <see cref="IOltInjectableSingleton"/>, and <see cref="IOltInjectableTransient"/> to associated DI by name 
        /// </summary>
        /// <remarks>
        /// Adds <see cref="IOltDbAuditUser"/> to resolve to <see cref="IOltIdentity"/> as scoped
        /// </remarks>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="baseAssemblies">List of Assemblies To Scan for interfaces</param>
        /// <returns><seealso cref="IServiceCollection"/></returns>
        public static IServiceCollection AddOltInjection(this IServiceCollection services, List<Assembly> baseAssemblies)
        {
            baseAssemblies.Add(Assembly.GetEntryAssembly());
            baseAssemblies.Add(Assembly.GetExecutingAssembly());
            var assembliesToScan = baseAssemblies.GetAllReferencedAssemblies();
            return services
                .Scan(sc =>
                    sc.FromAssemblies(assembliesToScan)
                        .AddClasses(classes => classes.AssignableTo<IOltInjectableScoped>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                        .AddClasses(classes => classes.AssignableTo<IOltInjectableTransient>())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                        .AddClasses(classes => classes.AssignableTo<IOltInjectableSingleton>())
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime())
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());
        }
    }
}
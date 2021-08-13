using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public static class OltServiceCollectionAutoMapperExtensions
    {
        public static IServiceCollection AddOltInjectionAutoMapper(this IServiceCollection services)
        {
            return services.AddOltInjectionAutoMapper(new List<Assembly>());
        }

        public static IServiceCollection AddOltInjectionAutoMapper(this IServiceCollection services, Assembly includeAssemblyScan)
        {
            return AddOltInjectionAutoMapper(services, new List<Assembly> { includeAssemblyScan });
        }

        public static IServiceCollection AddOltInjectionAutoMapper(this IServiceCollection services, List<Assembly> includeAssembliesScan)
        {
            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetEntryAssembly(),
                Assembly.GetExecutingAssembly()
            };

            assembliesToScan.AddRange(includeAssembliesScan);

            services.AddSingleton<IOltAdapterResolver, OltAdapterResolverAutoMapper>();
            services.AddAutoMapper(assembliesToScan.GetAllReferencedAssemblies());
            return services;
        }
    }
}

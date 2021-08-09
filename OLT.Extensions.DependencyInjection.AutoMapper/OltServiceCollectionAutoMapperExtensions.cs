using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public static class OltServiceCollectionAutoMapperExtensions
    {
        public static IServiceCollection AddOltAutoMapper(this IServiceCollection services)
        {
            return services.AddOltAutoMapper(new List<Assembly>());
        }

        public static IServiceCollection AddOltAutoMapper(this IServiceCollection services, List<Assembly> includeAssembliesScan)
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

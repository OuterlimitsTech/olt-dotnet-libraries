using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
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
            return services.AddOltInjectionAutoMapper(new List<Assembly> { includeAssemblyScan });
        }

        public static IServiceCollection AddOltInjectionAutoMapper(this IServiceCollection services,
            List<Assembly> includeAssembliesScan)
        {
            return services.AddOltInjectionAutoMapper(includeAssembliesScan, null);
        }

        public static IServiceCollection AddOltInjectionAutoMapper(this IServiceCollection services, List<Assembly> includeAssembliesScan, Action<IMapperConfigurationExpression> configAction, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetEntryAssembly(),
                Assembly.GetExecutingAssembly()
            };

            assembliesToScan.AddRange(includeAssembliesScan);

            services.AddSingleton<IOltAdapterResolver, OltAdapterResolverAutoMapper>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddCollectionMappers();
                configAction?.Invoke(cfg);
            }, assembliesToScan.GetAllReferencedAssemblies(), serviceLifetime);
            return services;
        }

        
    }
}

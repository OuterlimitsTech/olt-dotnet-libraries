using System;
using Microsoft.Extensions.DependencyInjection;
using OLT.Core;

namespace OLT.Extensions.DependencyInjection.AutoMapper
{
    public static class OltServiceCollectionAutoMapperExtensions
    {
        public static IServiceCollection AddOltAutoMapper(this IServiceCollection services)
        {
            var assembliesToScan = services.OltScanAssemblies();
            services.AddSingleton<IOltAdapterResolver, OltAdapterResolverAutoMapper>();
            services.AddAutoMapper(assembliesToScan);
            return services;
        }
    }
}

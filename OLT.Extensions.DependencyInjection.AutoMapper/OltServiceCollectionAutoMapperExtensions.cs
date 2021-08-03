using System;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
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

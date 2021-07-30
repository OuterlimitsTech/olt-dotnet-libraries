using Microsoft.Extensions.DependencyInjection;
using OLT.Core;
using OLT.Extensions.DependencyInjection.AutoMapper;
using OLT.Libraries.UnitTest.LocalServices;

namespace OLT.Libraries.UnitTest.Extensions
{
    public static class OltUnitTestServiceCollectionExtensions
    {
        /// <summary>
        /// Build Default AspNetCore Service and configures Dependency Injection
        /// AddOltDefault()
        /// AddOltAutoMapper()
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltUnitTesting(this IServiceCollection services, IOltInjectionOptions options)
        {
            services
                .AddOltDefault(options)
                .AddOltAutoMapper()
                .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());                
            return services;
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.LocalServices;

namespace OLT.Libraries.UnitTest.Assests.Extensions
{
    public static class OltUnitTestServiceCollectionExtensions
    {
        /// <summary>
        /// Build Default AspNetCore Service and configures Dependency Injection
        /// AddOltDefault()
        /// AddOltAutoMapper()
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltUnitTesting(this IServiceCollection services, Action action)
        {
            services
                .AddOltAddMemoryCache()
                .AddOltDefault(action)
                .AddOltAutoMapper()
                .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());                
            return services;
        }
    }
}
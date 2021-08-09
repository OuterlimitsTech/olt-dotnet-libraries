using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Extensions
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
        public static IServiceCollection AddOltUnitTesting(this IServiceCollection services)
        {
            services
                .AddOltAddMemoryCache()
                .AddOltDefault()
                .AddOltAutoMapper()
                .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());                
            return services;
        }
    }
}
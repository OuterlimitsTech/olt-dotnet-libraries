using System;
using System.Reflection;
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
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltUnitTesting(this IServiceCollection services)
        {
            services
                //.AddOltAddMemoryCache(TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30))
                .AddOltInjection()
                .AddOltInjectionAutoMapper()
                .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());             
            
            return services;
        }

        /// <summary>
        /// Build Default AspNetCore Service and configures Dependency Injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="includeAssemblyScan"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltUnitTesting(this IServiceCollection services, Assembly includeAssemblyScan)
        {
            services
                //.AddOltAddMemoryCache(TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30))
                .AddOltInjection()
                .AddOltInjectionAutoMapper(includeAssemblyScan)
                .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>());

            return services;
        }
    }
}
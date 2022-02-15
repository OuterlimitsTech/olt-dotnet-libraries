using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public static partial class OltCorsExtensions
    {

        /// <summary>
        /// Scans provided assemblies and thier referenced assemblies looking for <seealso cref="IOltAspNetCoreCorsPolicy"/> and adds the Cors policy
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="assembliesToScan">List of Asse</param>
        /// <returns><seealso cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCors(this IServiceCollection services, List<Assembly> assembliesToScan)
        {
            assembliesToScan
                .GetAllReferencedAssemblies()
                .GetAllImplements<IOltAspNetCoreCorsPolicy>()
                .ToList()
                .ForEach(policy => services.AddCors(policy));

            return services;
        }


        /// <summary>
        /// Adds CORS policy
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="policy"><seealso cref="IOltAspNetCoreCorsPolicy"/></param>
        /// <returns><seealso cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCors(this IServiceCollection services, IOltAspNetCoreCorsPolicy policy)
        {
            return policy.AddCors(services);
        }

        /// <summary>
        /// Registers middleware <seealso cref="CorsMiddlewareExtensions"/> using <seealso cref="IOltOptionsAspNetHosting.CorsPolicyName"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsAspNetHosting"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseCors<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetHosting
        {
            return options.CorsPolicyName.IsNotEmpty() ? app.UseCors(options.CorsPolicyName) : app;
        }
    }
}

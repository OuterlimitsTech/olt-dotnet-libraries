using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public static partial class OltApplicationBuilderSettingsExtensions
    {
        /// <summary>
        /// Registers middleware <seealso cref="UsePathBaseExtensions"/>  using <seealso cref="IOltOptionsAspNetHosting.PathBase"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsAspNetHosting"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UsePathBase<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetHosting
        {
            return options.PathBase.IsNotEmpty() ? app.UsePathBase(options.PathBase) : app;
        }


        /// <summary>
        /// Registers middleware <seealso cref="DeveloperExceptionPageExtensions"/> using <seealso cref="IOltOptionsAspNetHosting.ShowExceptionDetails"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsAspNetHosting"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseDeveloperExceptionPage<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetHosting
        {
            return options.ShowExceptionDetails ? app.UseDeveloperExceptionPage() : app;
        }

        /// <summary>
        /// Registers middleware <seealso cref="DeveloperExceptionPageExtensions"/> using <seealso cref="IOltOptionsAspNetHosting.ShowExceptionDetails"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsAspNetHosting"/></param>
        /// <param name="pageOptions"><seealso cref="DeveloperExceptionPageOptions"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseDeveloperExceptionPage<TOptions>(this IApplicationBuilder app, TOptions options, DeveloperExceptionPageOptions pageOptions)
            where TOptions : IOltOptionsAspNetHosting
        {
            return options.ShowExceptionDetails ? app.UseDeveloperExceptionPage(pageOptions) : app;
        }

        /// <summary>
        /// Registers middleware <seealso cref="HstsBuilderExtensions"/> using <seealso cref="IOltOptionsAspNetHosting.UseHsts"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsAspNetHosting"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseHsts<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetHosting
        {
            return options.UseHsts ? app.UseHsts() : app;
        }



        /// <summary>
        /// Registers middleware <seealso cref="HttpsPolicyBuilderExtensions"/> using <seealso cref="IOltOptionsAspNetHosting.DisableHttpsRedirect"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsAspNetHosting"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseHttpsRedirection<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetHosting
        {
            return options.DisableHttpsRedirect ? app : app.UseHttpsRedirection();
        }

    
    }
}

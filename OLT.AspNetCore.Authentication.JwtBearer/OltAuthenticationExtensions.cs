using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OLT.AspNetCore.Authentication;

namespace OLT.Core
{
    public static partial class OltAuthenticationExtensions
    {
        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationJwtBearer"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddJwtBearer<TOptions>(this IServiceCollection services, TOptions options)
            where TOptions : IOltAuthenticationJwtBearer, IOltAuthenticationSchemeBuilder<JwtBearerOptions>
        {
            return services.AddAuthentication(options, null, null);
        }

        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationJwtBearer"/></param>
        /// <param name="configureOptions"><seealso cref="JwtBearerOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddJwtBearer<TOptions>(this IServiceCollection services, TOptions options, Action<JwtBearerOptions> configureOptions)
            where TOptions : IOltAuthenticationJwtBearer, IOltAuthenticationSchemeBuilder<JwtBearerOptions>
        {
            return services.AddAuthentication(options, null, configureOptions);
        }

        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationJwtBearer"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options)
            where TOptions : IOltAuthenticationJwtBearer, IOltAuthenticationSchemeBuilder<JwtBearerOptions>
        {
            return services.AddJwtBearer(options, null);
        }

        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationJwtBearer"/></param>
        /// <param name="configureOptions"><seealso cref="JwtBearerOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options, Action<JwtBearerOptions> configureOptions)
            where TOptions : IOltAuthenticationJwtBearer, IOltAuthenticationSchemeBuilder<JwtBearerOptions>
        {
            return services.AddJwtBearer(options, configureOptions);
        }


        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationJwtBearer"/></param>
        /// <param name="configureAuthenticationOptions"><seealso cref="AuthenticationOptions"/></param>
        /// <param name="configureJwtBearerOptions"><seealso cref="JwtBearerOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options, Action<AuthenticationOptions> configureAuthenticationOptions, Action<JwtBearerOptions> configureJwtBearerOptions)
            where TOptions : IOltAuthenticationJwtBearer, IOltAuthenticationSchemeBuilder<JwtBearerOptions>
        {
            return options.Disabled ? new AuthenticationBuilder(services) : options.AddScheme(options.AddAuthentication(services, configureAuthenticationOptions, false), configureJwtBearerOptions);
        }

    }
}
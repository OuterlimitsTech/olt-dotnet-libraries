using System;
using System.Text;
using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
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
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddJwtBearer<TOptions>(this IServiceCollection services, TOptions options)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
        {
            return services.AddAuthentication(options, null, null);
        }

        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <param name="configureOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddJwtBearer<TOptions>(this IServiceCollection services, TOptions options, Action<ApiKeyOptions> configureOptions)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
        {
            return services.AddAuthentication(options, null, configureOptions);
        }

        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
        {
            return services.AddJwtBearer(options, null);
        }

        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <param name="configureOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options, Action<ApiKeyOptions> configureOptions)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
        {
            return services.AddJwtBearer(options, configureOptions);
        }


        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <param name="configureAuthenticationOptions"><seealso cref="AuthenticationOptions"/></param>
        /// <param name="configureApiKeyOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options, Action<AuthenticationOptions> configureAuthenticationOptions, Action<ApiKeyOptions> configureApiKeyOptions)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
        {
            return options.Disabled ? new AuthenticationBuilder(services) : options.AddScheme(options.AddAuthentication(services, configureAuthenticationOptions, false), configureApiKeyOptions);
        }

    }
}
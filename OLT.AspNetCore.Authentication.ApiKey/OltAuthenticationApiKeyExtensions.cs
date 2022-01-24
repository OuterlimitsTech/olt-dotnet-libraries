using System;
using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.AspNetCore.Authentication
{
    public static class OltAuthenticationApiKeyExtensions
    {
        /// <summary>
        /// Adds API Key Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddApiKey<TOptions>(this IServiceCollection services, TOptions options)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
            => services.AddApiKey(options, null, null);

        /// <summary>
        /// Adds API Key Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <param name="apiKeyOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddApiKey<TOptions>(this IServiceCollection services, TOptions options, Action<ApiKeyOptions> apiKeyOptions)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
            => services.AddApiKey(options, apiKeyOptions, null);

        /// <summary>
        /// Adds API Key Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <param name="authOptionsAction"><seealso cref="AuthenticationOptions"/></param>
        /// <param name="schemeOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddApiKey<TOptions>(this IServiceCollection services, TOptions options, Action<ApiKeyOptions> schemeOptions, Action<AuthenticationOptions> authOptionsAction)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            var builder = options.AddAuthentication(services, authOptionsAction);
            return options.AddScheme(builder, schemeOptions);
        }

    }
}
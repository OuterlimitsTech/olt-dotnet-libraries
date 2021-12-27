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
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddAuthentication(options, null, null);
        }

        /// <summary>
        /// Adds API Key Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <param name="configureOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddApiKey<TOptions>(this IServiceCollection services, TOptions options, Action<ApiKeyOptions> configureOptions)
            where TOptions : IOltAuthenticationApiKey, IOltAuthenticationSchemeBuilder<ApiKeyOptions>
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            return services.AddAuthentication(options, null, configureOptions);
        }

        /// <summary>
        /// Adds API Key Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options)
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
            return services.AddApiKey(options, null);
        }

        /// <summary>
        /// Adds API Key Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <param name="configureOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options, Action<ApiKeyOptions> configureOptions)
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
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }
            return services.AddApiKey(options, configureOptions);
        }


        /// <summary>
        /// Adds API Key Authentication
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><seealso cref="IOltAuthenticationApiKey"/></param>
        /// <param name="configureAuthenticationOptions"><seealso cref="AuthenticationOptions"/></param>
        /// <param name="configureApiKeyOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static AuthenticationBuilder AddAuthentication<TOptions>(this IServiceCollection services, TOptions options, Action<AuthenticationOptions> configureAuthenticationOptions, Action<ApiKeyOptions> configureApiKeyOptions)
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
            return options.AddScheme(options.AddAuthentication(services, configureAuthenticationOptions), configureApiKeyOptions);
        }

    }
}
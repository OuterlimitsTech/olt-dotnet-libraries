﻿using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using OLT.AspNetCore.Authentication;

namespace OLT.Core
{
    public static partial class OltAuthenticationJwtExtensions
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
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }  
            return services.AddJwtBearer(options, null, null);
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
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }
            return services.AddJwtBearer(options, configureOptions, null);
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
        public static AuthenticationBuilder AddJwtBearer<TOptions>(this IServiceCollection services, TOptions options, Action<JwtBearerOptions> configureJwtBearerOptions, Action<AuthenticationOptions> configureAuthenticationOptions)
            where TOptions : IOltAuthenticationJwtBearer, IOltAuthenticationSchemeBuilder<JwtBearerOptions>
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            return options.AddScheme(options.AddAuthentication(services, configureAuthenticationOptions), configureJwtBearerOptions);
        }

    }
}
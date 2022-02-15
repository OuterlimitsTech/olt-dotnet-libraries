using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.AspNetCore.Authentication
{
    public interface IOltAuthenticationBuilder
    {

        /// <summary>
        /// Adds Authentication setting <seealso cref="AuthenticationOptions.DefaultAuthenticateScheme"/> and <seealso cref="AuthenticationOptions.DefaultChallengeScheme"/> to <seealso cref="OltAuthenticationBuilder.Scheme"/>
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        AuthenticationBuilder AddAuthentication(IServiceCollection services);

        /// <summary>
        /// Adds Authentication setting <seealso cref="AuthenticationOptions.DefaultAuthenticateScheme"/> and <seealso cref="AuthenticationOptions.DefaultChallengeScheme"/> to <seealso cref="OltAuthenticationBuilder.Scheme"/>
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="configureOptions"><seealso cref="AuthenticationOptions" /></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        AuthenticationBuilder AddAuthentication(IServiceCollection services, Action<AuthenticationOptions> configureOptions);
    }
}
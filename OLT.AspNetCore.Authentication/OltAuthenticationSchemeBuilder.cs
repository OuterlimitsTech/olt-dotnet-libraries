using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.AspNetCore.Authentication
{
    public abstract class OltAuthenticationBuilder : IOltAuthenticationBuilder
    {
        public abstract string Scheme { get; }

        /// <summary>
        /// Adds Authentication setting <seealso cref="AuthenticationOptions.DefaultAuthenticateScheme"/> and <seealso cref="AuthenticationOptions.DefaultChallengeScheme"/> to <seealso cref="OltAuthenticationSchemeBuilder.Scheme"/>
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual AuthenticationBuilder AddAuthentication(IServiceCollection services)
        {
            return AddAuthentication(services, null);
        }


        /// <summary>
        /// Adds Authentication setting <seealso cref="AuthenticationOptions.DefaultAuthenticateScheme"/> and <seealso cref="AuthenticationOptions.DefaultChallengeScheme"/> to <seealso cref="OltAuthenticationSchemeBuilder.Scheme"/>
        /// </summary>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="configureOptions"><seealso cref="AuthenticationOptions" /></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public virtual AuthenticationBuilder AddAuthentication(IServiceCollection services, Action<AuthenticationOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = Scheme;
                    opt.DefaultChallengeScheme = Scheme;
                    configureOptions?.Invoke(opt);
                });
        }

    }



    public abstract class OltAuthenticationSchemeBuilder<TSchemeOption> : OltAuthenticationBuilder, IOltAuthenticationSchemeBuilder<TSchemeOption>
        where TSchemeOption : AuthenticationSchemeOptions
    {

        /// <summary>
        /// Adds Authentication 
        /// </summary>
        /// <typeparam name="TSchemeOption"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public virtual AuthenticationBuilder AddScheme(AuthenticationBuilder builder)
        {
            return AddScheme(builder, null);
        }

        /// <summary>
        /// Adds Authentication 
        /// </summary>
        /// <typeparam name="TSchemeOption"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public abstract AuthenticationBuilder AddScheme(AuthenticationBuilder builder, Action<TSchemeOption> configureOptions);

    }
}
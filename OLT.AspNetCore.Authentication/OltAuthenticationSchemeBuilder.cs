using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.AspNetCore.Authentication
{
    public abstract class OltAuthenticationSchemeBuilder : IOltAuthenticationBuilder
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
        public virtual AuthenticationBuilder AddAuthentication(IServiceCollection services, Action<AuthenticationOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var builder = services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = Scheme;
                    opt.DefaultChallengeScheme = Scheme;
                    configureOptions?.Invoke(opt);
                });

            return AddScheme(builder);
        }

        public abstract AuthenticationBuilder AddScheme(AuthenticationBuilder builder);
    }

    public abstract class OltAuthenticationSchemeBuilder<TSchemeOption> : OltAuthenticationSchemeBuilder, IOltAuthenticationSchemeBuilder<TSchemeOption>
        where TSchemeOption : AuthenticationSchemeOptions
    {

        public override AuthenticationBuilder AddScheme(AuthenticationBuilder builder)
        {
            return AddScheme(builder, null);
        }

        public abstract AuthenticationBuilder AddScheme(AuthenticationBuilder builder, Action<TSchemeOption> configureOptions);

    }
}
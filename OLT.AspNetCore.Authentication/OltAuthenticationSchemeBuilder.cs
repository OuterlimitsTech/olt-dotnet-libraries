﻿using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.AspNetCore.Authentication
{
    public abstract class OltAuthenticationSchemeBuilder : IOltAuthenticationBuilder
    {
        public abstract string Scheme { get; }

        public virtual AuthenticationBuilder AddAuthentication(IServiceCollection services)
        {
            return AddAuthentication(services, null);
        }

        public virtual AuthenticationBuilder AddAuthentication(IServiceCollection services, Action<AuthenticationOptions> configureOptions)
        {
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
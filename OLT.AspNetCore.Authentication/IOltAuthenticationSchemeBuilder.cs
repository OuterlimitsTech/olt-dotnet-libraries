using System;
using Microsoft.AspNetCore.Authentication;

namespace OLT.AspNetCore.Authentication
{
    public interface IOltAuthenticationSchemeBuilder : IOltAuthenticationScheme
    {
        bool Disabled { get; }
        AuthenticationBuilder AddScheme(AuthenticationBuilder builder);
    }

    public interface IOltAuthenticationSchemeBuilder<out TSchemeOption> : IOltAuthenticationSchemeBuilder
        where TSchemeOption : AuthenticationSchemeOptions
    {
        AuthenticationBuilder AddScheme(AuthenticationBuilder builder, Action<TSchemeOption> configureOptions);
    }
}
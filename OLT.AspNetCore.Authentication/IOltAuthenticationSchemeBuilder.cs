using System;
using Microsoft.AspNetCore.Authentication;

namespace OLT.AspNetCore.Authentication
{
    public interface IOltAuthenticationSchemeBuilder
    {
        AuthenticationBuilder AddScheme(AuthenticationBuilder builder);
    }

    public interface IOltAuthenticationSchemeBuilder<out TSchemeOption> : IOltAuthenticationSchemeBuilder, IOltAuthenticationBuilder
        where TSchemeOption : AuthenticationSchemeOptions
    {        
        AuthenticationBuilder AddScheme(AuthenticationBuilder builder, Action<TSchemeOption> configureOptions);
    }
}
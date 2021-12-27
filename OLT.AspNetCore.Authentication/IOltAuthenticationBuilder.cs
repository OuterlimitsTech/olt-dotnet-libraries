using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.AspNetCore.Authentication
{
    public interface IOltAuthenticationBuilder
    {
        AuthenticationBuilder AddAuthentication(IServiceCollection services);
        AuthenticationBuilder AddAuthentication(IServiceCollection services, Action<AuthenticationOptions> configureOptions);
    }
}
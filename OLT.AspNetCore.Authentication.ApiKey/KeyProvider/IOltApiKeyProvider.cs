using AspNetCore.Authentication.ApiKey;
using OLT.Core;

namespace OLT.AspNetCore.Authentication
{
    public interface IOltApiKeyProvider : IApiKeyProvider, IOltInjectableScoped
    {
        
    }
}
using AspNetCore.Authentication.ApiKey;
using Microsoft.Extensions.Logging;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using System.Threading.Tasks;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication.ApiKey
{
    public class ApiKeyProvider : OltApiKeyProvider<ApiKeyService>
    {
        public ApiKeyProvider(ApiKeyService service, ILogger<ApiKeyService> logger) : base(service, logger)
        {
        }
    }

    public class ApiKeyService : OltCoreService, IOltApiKeyService
    {
        public ApiKeyService(IOltServiceManager serviceManager) : base(serviceManager)
        {
        }

        public Task<IApiKey> ValidateAsync(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}

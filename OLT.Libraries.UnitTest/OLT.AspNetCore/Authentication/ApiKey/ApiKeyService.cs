using AspNetCore.Authentication.ApiKey;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using System.Threading.Tasks;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication.ApiKey
{
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

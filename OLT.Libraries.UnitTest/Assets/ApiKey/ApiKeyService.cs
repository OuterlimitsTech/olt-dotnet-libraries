using System.Threading.Tasks;
using AspNetCore.Authentication.ApiKey;
using OLT.AspNetCore.Authentication;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.ApiKey
{
    // ReSharper disable once InconsistentNaming
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
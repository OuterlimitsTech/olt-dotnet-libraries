using AspNetCore.Authentication.ApiKey;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication.ApiKey
{
    public class ApiKeyResult : IApiKey
    {
        public ApiKeyResult(List<Claim> claims)
        {
            Claims = claims;
        }

        public string Key { get; set; }
        public string OwnerName { get; set; }
        public IReadOnlyCollection<Claim> Claims { get; }
    }

    public class ApiKeyProvider : OltApiKeyProvider<ApiKeyService>
    {
        public ApiKeyProvider(ApiKeyService service, ILogger<ApiKeyService> logger) : base(service, logger)
        {
        }
    }

    public class ApiKeyService : OltDisposable, IOltApiKeyService
    {
        //private readonly SqlDatabaseContext _context;

        //public ApiKeyService(SqlDatabaseContext context)
        //{
        //    _context = context;
        //}

        public Task<IApiKey> ValidateAsync(string key)
        {
            var ownerName = "Test Api Key";

            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, ownerName, ClaimValueTypes.String)
            };

            var result = new ApiKeyResult(claimsList)
            {
                Key = key,
                OwnerName = ownerName
            };


            return Task.FromResult<IApiKey>(result);
        }
    }
}

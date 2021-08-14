using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Authentication.ApiKey;
using Microsoft.Extensions.Logging;
using OLT.Core;

namespace OLT.AspNetCore.Authentication
{
    public class OltApiKeyProvider<TService> : OltDisposable, IOltApiKeyProvider
        where TService : class, IOltApiKeyService
    {
        private readonly ILogger<TService> _logger;
        private readonly TService _service;

        public OltApiKeyProvider(
            TService service,
            ILogger<TService> logger)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IApiKey> ProvideAsync(string key)
        {
            try
            {
                return await _service.ValidateAsync(key);
            }
            catch (System.Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}
using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication.ApiKey
{

    public class ApiKeyTests
    {
        public static string Relm = "OLT Unit Test API";


        private OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>> GetOptions(string keyName)
        {
            return new OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>(Relm)
            {
                KeyName = keyName
            };
        }


        [Fact]
        public void ArgumentExceptions()
        {
            var services = new ServiceCollection();
            var options = GetOptions("X-API-KEY-TEST");
            Action<ApiKeyOptions> apiKeyOptionsAction = (ApiKeyOptions opts) =>
            {
                opts.Realm = "OLT Manual Realm";
            };

            Action<AuthenticationOptions> authOptionsAction = (AuthenticationOptions opts) =>
            {
                opts.DefaultScheme = ApiKeyDefaults.AuthenticationScheme;
            };

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddApiKey(null, options));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null, null, null));

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddApiKey(null, options, null));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null, apiKeyOptionsAction));

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddApiKey(null, options, apiKeyOptionsAction, authOptionsAction));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null, apiKeyOptionsAction, authOptionsAction));

            Assert.Throws<ArgumentNullException>("builder", () => options.AddScheme(null));
            Assert.Throws<ArgumentNullException>("services", () => options.AddAuthentication(null));


        }


        [Fact]
        public void Options()
        {
            var services = new ServiceCollection();
            var builder = services.AddAuthentication();
            var invalidLocation = -1000;
            var invalid = new OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>(Relm, (OltApiKeyLocation)invalidLocation);
            Assert.Throws<ArgumentNullException>("builder", () => invalid.AddScheme(null, null));
            Assert.Throws<InvalidOperationException>(() => invalid.AddScheme(builder, null));
        }

        private async Task ApiAuthTest<T>(TestServer testServer) where T : class
        {
            var services = testServer.Host.Services;
            var schemeProvider = services.GetRequiredService<IAuthenticationSchemeProvider>();
            Assert.NotNull(schemeProvider);
            var scheme = await schemeProvider.GetDefaultAuthenticateSchemeAsync();
            Assert.NotNull(scheme);
            Assert.Equal(typeof(T), scheme.HandlerType);

            var apiKeyOptionsSnapshot = services.GetService<IOptionsSnapshot<ApiKeyOptions>>();
            var apiKeyOptions = apiKeyOptionsSnapshot.Get(scheme.Name);
            Assert.NotNull(apiKeyOptions);
            Assert.Equal(ApiKeyConstants.Realm, apiKeyOptions.Realm);

            Assert.NotNull(services.GetService<IOltApiKeyService>());
            Assert.NotNull(services.GetService<IOltApiKeyProvider>());
            Assert.NotNull(services.GetService<IApiKeyProvider>());
        }

        [Fact]
        public async Task ApiKeyStartupDefault()
        {
            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<ApiKeyStartupDefault>()))
            {
                await ApiAuthTest<ApiKeyInHeaderOrQueryParamsHandler>(testServer);
            }
        }


        [Fact]
        public async Task ApiKeyStartupQueryParamsOnly()
        {
            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<ApiKeyStartupQueryParamsOnly>()))
            {
                await ApiAuthTest<ApiKeyInQueryParamsHandler>(testServer);
            }

            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<ApiKeyStartupQueryParamsOnlyWithOptions>()))
            {
                await ApiAuthTest<ApiKeyInHeaderHandler>(testServer);
            }
        }

        [Fact]
        public async Task ApiKeyStartupHeaderOnly()
        {
            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<ApiKeyStartupHeaderOnly>()))
            {
                await ApiAuthTest<ApiKeyInHeaderHandler>(testServer);
            }

            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<ApiKeyStartupHeaderOnlyWithOptions>()))
            {
                await ApiAuthTest<ApiKeyInHeaderHandler>(testServer);
            }
        }

        [Fact]
        public async Task ApiKeyStartupHeaderOrQueryParams()
        {
            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<ApiKeyStartupHeaderOrQueryParams>()))
            {
                await ApiAuthTest<ApiKeyInHeaderOrQueryParamsHandler>(testServer);
            }

            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<ApiKeyStartupHeaderOrQueryParamsWithOptions>()))
            {
                await ApiAuthTest<ApiKeyInHeaderOrQueryParamsHandler>(testServer);
            }
        }
    }
}

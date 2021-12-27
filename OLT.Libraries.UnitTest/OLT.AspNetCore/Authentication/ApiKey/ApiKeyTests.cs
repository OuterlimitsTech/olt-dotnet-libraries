using AspNetCore.Authentication.ApiKey;
using Microsoft.Extensions.DependencyInjection;
using OLT.AspNetCore.Authentication;
using System;
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
            Action<ApiKeyOptions> action = (ApiKeyOptions opts) =>
            {
                opts.Realm = "OLT Manual Realm";
            };

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddApiKey(null, options));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null));


            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddApiKey(null, options, null));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null, action));
            Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationApiKeyExtensions.AddApiKey(services, options, null));


            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddAuthentication(null, options));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddAuthentication<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null));

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddAuthentication(null, options, null));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddAuthentication<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null, action));
            Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationApiKeyExtensions.AddAuthentication(services, options, null));

        }
    }
}

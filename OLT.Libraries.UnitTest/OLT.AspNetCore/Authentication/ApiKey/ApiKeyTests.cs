using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using OLT.AspNetCore.Authentication;
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
            Action<ApiKeyOptions> action = (ApiKeyOptions opts) =>
            {
                opts.Realm = "OLT Manual Realm";
            };

            Action<AuthenticationOptions> authAction = (AuthenticationOptions opts) =>
            {
                opts.DefaultScheme = ApiKeyDefaults.AuthenticationScheme;
            };

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddApiKey(null, options));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null));


            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddApiKey(null, options, null));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null, action));
            Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationApiKeyExtensions.AddApiKey(services, options, null));


            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationApiKeyExtensions.AddApiKey(null, options, action, authAction));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationApiKeyExtensions.AddApiKey<OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>>(services, null, action, authAction));

            Assert.Throws<ArgumentNullException>("builder", () => options.AddScheme(null));
            Assert.Throws<ArgumentNullException>("configureOptions", () => options.AddScheme(services.AddAuthentication(options.Scheme), null));

            Assert.Throws<ArgumentNullException>("services", () => options.AddAuthentication(null));

        }


        ////[Fact]
        ////public async Task ApiAuth()
        ////{

        ////    using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<ApiKeyStartup>()))
        ////    {
        ////        var t = testServer.CreateRequest("");
        ////        var result = await testServer.SendAsync(context =>
        ////        {
        ////            context.Request.Headers.Add("X-API-KEY", ApiKeyStartup.Key1);                       
        ////        });

        ////    }

        ////    //var context = new DefaultHttpContext();
        ////    //context.Request.Headers.Add();
        ////    //var controller = new OltTestController()
        ////    //{
        ////    //    ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext()
        ////    //    {
        ////    //        HttpContext = context
        ////    //    }
        ////    //};

        ////    //var actionResult = controller.GetSimple();
        ////    //Assert.NotNull(actionResult);

        ////}
    }
}

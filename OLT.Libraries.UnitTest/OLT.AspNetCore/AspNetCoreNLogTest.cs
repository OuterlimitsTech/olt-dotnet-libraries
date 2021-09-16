using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OLT.Core;
using OLT.Logging.NLog;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore
{

    //https://www.roundthecode.com/dotnet/asp-net-core-web-api/asp-net-core-testserver-xunit-test-web-api-endpoints

    // ReSharper disable once InconsistentNaming
    public class AspNetCoreNLogTest : OltDisposable
    {
        private readonly TestServer _testServer;

        public AspNetCoreNLogTest()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddUserSecrets<Startup>()
                        .AddJsonFile("appsettings.json", false, true)
                        .AddEnvironmentVariables();
                })
                .UseStartup<NLogStartup>();
            _testServer = new TestServer(webBuilder);
        }

        [Fact]
        public async Task TestLogging()
        {
            var response = await _testServer.CreateRequest("/api/league/1").SendAsync("GET");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            response = await _testServer.CreateRequest("/api/league/2").SendAsync("GET");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void SerializeErrorHttp()
        {
            var obj = new OltErrorHttp
            {
                Message = Faker.Lorem.GetFirstWord(),
                Errors = new List<string>
                {
                    Faker.Lorem.GetFirstWord(),
                    Faker.Lorem.GetFirstWord()
                }
            };

            var compareTo = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()});
            Assert.Equal(obj.ToString(), compareTo);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _testServer.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}
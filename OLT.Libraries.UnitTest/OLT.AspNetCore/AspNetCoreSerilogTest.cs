using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using OLT.Core;
using Serilog;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore
{

    // ReSharper disable once InconsistentNaming
    public class AspNetCoreSerilogTest : OltDisposable
    {
        private readonly TestServer _testServer;

        public AspNetCoreSerilogTest()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder
                .UseSerilog()
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddUserSecrets<Startup>()
                        .AddJsonFile("appsettings.json", false, true)
                        .AddEnvironmentVariables();
                })
                .UseStartup<SerilogStartup>();
                
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

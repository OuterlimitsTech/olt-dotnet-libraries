using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLT.Core;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore
{
    public class AspNetCoreTests : OltDisposable
    {
        private readonly TestServer _testServer;

        public AspNetCoreTests()

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
                .UseStartup<TestServerStartup>();
            _testServer = new TestServer(webBuilder);
        }


        [Fact]
        public void ResolveRelativePath()
        {
            var environment = _testServer.Services.GetService<IWebHostEnvironment>();
            var host = _testServer.Services.GetService<IOltHostService>();
            Assert.Equal(environment?.WebRootPath, host?.ResolveRelativePath("~/"));
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
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using OLT.Core;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore
{

    //https://www.roundthecode.com/dotnet/asp-net-core-web-api/asp-net-core-testserver-xunit-test-web-api-endpoints

    // ReSharper disable once InconsistentNaming
    public class AspNetCoreServerTest : OltDisposable
    {
        private readonly TestServer _testServer;

        public AspNetCoreServerTest()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<NLogStartup>();
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


    //// ReSharper disable once InconsistentNaming
    //public class AspNetCoreNLogTest : IDisposable
    //{
    //    public AspNetCoreNLogTest()
    //    {
    //    }

    //    [Fact]
    //    public async Task TestLogging()
    //    {
    //        var response = await _testServer.CreateRequest("/api/league/1").SendAsync("GET");
    //        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    //        response = await _testServer.CreateRequest("/api/league/2").SendAsync("GET");
    //        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    //    }

    //    public void Dispose()
    //    {
    //        _testServer.Dispose();
    //    }
    //}
}
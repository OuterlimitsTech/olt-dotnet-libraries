using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        [Fact]
        public void Environment()
        {
            Assert.Throws<ArgumentNullException>("hostEnvironment", () => OltHostEnvironmentExtensions.IsTest(null));

            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<TestServerStartup>()))
            {
                var environment = _testServer.Services.GetService<IWebHostEnvironment>();                
                Assert.True(OltHostEnvironmentExtensions.IsTest(environment));
                var host = _testServer.Services.GetService<IOltHostService>();
                Assert.Equal(environment?.EnvironmentName, host?.EnvironmentName);
                Assert.False(host?.Environment.IsProduction);
                Assert.False(host?.Environment.IsDevelopment);
                Assert.False(host?.Environment.IsStaging);
                Assert.True(host?.Environment.IsTest);

                //environment.EnvironmentName = OltDefaults.OltEnvironments.Test;
                //Assert.False(host?.Environment.IsProduction);
                //Assert.False(host?.Environment.IsDevelopment);
                //Assert.False(host?.Environment.IsStaging);
                //Assert.True(host?.Environment.IsTest);

                //environment.EnvironmentName = OltDefaults.OltEnvironments.Development;
                //Assert.False(host?.Environment.IsProduction);
                //Assert.True(host?.Environment.IsDevelopment);
                //Assert.False(host?.Environment.IsStaging);
                //Assert.False(host?.Environment.IsTest);

                //environment.EnvironmentName = OltDefaults.OltEnvironments.Staging;
                //Assert.False(host?.Environment.IsProduction);
                //Assert.False(host?.Environment.IsDevelopment);
                //Assert.True(host?.Environment.IsStaging);
                //Assert.False(host?.Environment.IsTest);

            }
        }

        [Fact]
        public void InternalServerError()
        {
            var expectedResult = new OltInternalServerErrorObjectResult();
            var controller = new OltTestController();
            var result = controller.TestInternalServerError(null);
            var viewResult = Assert.IsType<OltInternalServerErrorObjectResult>(result);            
            Assert.Equal(StatusCodes.Status500InternalServerError, viewResult.StatusCode);
            Assert.Null(viewResult.Value);

            expectedResult = new OltInternalServerErrorObjectResult("Test Message");
            result = controller.TestInternalServerError("Test Message");
            viewResult = Assert.IsType<OltInternalServerErrorObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, viewResult.StatusCode);
            Assert.Equal(expectedResult.Value, viewResult.Value);

        }

        [Fact]
        public void BadRequest()
        {
            var expectedResult = new OltErrorHttp {  Message = "Testing Bad Exception" };

            var controller = new OltTestController();
            var controllerResult = controller.TestBadRequest(null);
            var viewResult1 = Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(controllerResult);
            Assert.Equal(StatusCodes.Status400BadRequest, viewResult1.StatusCode);


            controllerResult = controller.TestBadRequest(expectedResult.Message);
            var viewResult2 = Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(controllerResult);
            Assert.Equal(StatusCodes.Status400BadRequest, viewResult2.StatusCode);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var jsonResult = System.Text.Json.JsonSerializer.Deserialize<OltErrorHttp>(viewResult2.Value.ToString().ToASCIIBytes(), options);
            jsonResult.Should().BeEquivalentTo(expectedResult);            
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
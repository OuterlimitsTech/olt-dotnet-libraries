using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Logging.Serilog;
using Serilog;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore
{
    public class TestJson
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }

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

            var compareTo = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            Assert.Equal(obj.ToString(), compareTo);
        }

        [Fact]
        public async Task OltMiddlewarePayload_Returns500StatusCode()
        {
            //arrange
            var expectedException = new ArgumentNullException();

            Task MockNextMiddleware(HttpContext context)
            {
                return Task.FromException(expectedException);
            }

            var httpContext = new DefaultHttpContext();

            var exceptionHandlingMiddleware = new OltMiddlewarePayload(MockNextMiddleware);

            //act
            await exceptionHandlingMiddleware.Invoke(httpContext);

            //assert
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task OltMiddlewarePayload_OltBadRequestException()
        {
            //arrange
            var expectedException = new OltBadRequestException("Test Bad Request");

            Task MockNextMiddleware(HttpContext context)
            {
                return Task.FromException(expectedException);
            }

            var httpContext = new DefaultHttpContext();

            var exceptionHandlingMiddleware = new OltMiddlewarePayload(MockNextMiddleware);

            //act
            await exceptionHandlingMiddleware.Invoke(httpContext);

            //assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task OltMiddlewarePayload_OltValidationException()
        {
            //arrange
            var expectedException = new OltValidationException(new List<IOltValidationError> { new OltValidationError { Message = "Test Validation" } });

            Task MockNextMiddleware(HttpContext context)
            {
                return Task.FromException(expectedException);
            }

            var httpContext = new DefaultHttpContext();

            var exceptionHandlingMiddleware = new OltMiddlewarePayload(MockNextMiddleware);

            //act
            await exceptionHandlingMiddleware.Invoke(httpContext);

            //assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task OltMiddlewarePayload_Completed()
        {
            Task MockNextMiddleware(HttpContext context)
            {
                return Task.CompletedTask;
            }

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("HelloWorld", "true");

            var exceptionHandlingMiddleware = new OltMiddlewarePayload(MockNextMiddleware);

            //act
            await exceptionHandlingMiddleware.Invoke(httpContext);

            //assert
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)httpContext.Response.StatusCode);
        }


        [Fact]
        public async Task UseSerilogRequestLogging1()
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
                .ConfigureServices((host, services) =>
                {
                    services
                        .AddOltAspNetCore(new AppSettingsDto(), this.GetType().Assembly, null)
                        .AddOltSerilog();
                })
                .Configure(app =>
                {
                    app.UseSerilogRequestLogging(
                        new OltOptionsAspNetSerilog
                        {
                            DisableMiddlewareRegistration = true
                        },
                        options =>
                        {
                            options.MessageTemplate =
                                "[{Timestamp:HH:mm:ss} {Level:u3}] {EventType:x8} {Message:lj}{NewLine}{Exception}";
                        });

                })
                .UseStartup<SerilogStartup>();
                

            var testServer = new TestServer(webBuilder);
            var response = await testServer.CreateRequest("/api/league/2").SendAsync("GET");
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

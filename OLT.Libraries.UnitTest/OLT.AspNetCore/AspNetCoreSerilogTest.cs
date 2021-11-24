using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        private IOptions<OltSerilogOptions> GetOptions(bool showExceptionDetails = false, string errorMsg = null)
        {
            var settings = new OltSerilogOptions
            {
                ShowExceptionDetails = showExceptionDetails,
            };

            if (errorMsg != null)
            {
                settings.ErrorMessage = errorMsg;
            }
            return Options.Create(settings);
        }

        
        private async Task<OltErrorHttp> InvokeMiddlewareAsync(IOptions<OltSerilogOptions> options, RequestDelegate next, HttpStatusCode expectedStatusCode)
        {
            return await InvokeMiddlewareAsync<OltErrorHttp>(options, next, expectedStatusCode);
        }

        private async Task<T> InvokeMiddlewareAsync<T>(IOptions<OltSerilogOptions> options, RequestDelegate next, HttpStatusCode expectedStatusCode)
        {
            var exceptionHandlingMiddleware = new OltMiddlewarePayload(options);
            var bodyStream = new MemoryStream();
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = bodyStream;
            httpContext.Request.Path = "/testing";

            //act
            await exceptionHandlingMiddleware.InvokeAsync(httpContext, next);

            T response;
            bodyStream.Seek(0, SeekOrigin.Begin);
            using (var sr = new StreamReader(bodyStream))
            {
                response = JsonConvert.DeserializeObject<T>(await sr.ReadToEndAsync());
            }

            Assert.Equal(expectedStatusCode, (HttpStatusCode)httpContext.Response.StatusCode);

            return response;
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
            var expectedMsg = "An error has occurred.";
            var overrideMsg = "Override Error Message";
            var expectedException = new ArgumentNullException();
            RequestDelegate next = (HttpContext hc) => Task.FromException(expectedException);       

            var response = await this.InvokeMiddlewareAsync(GetOptions(true), next, HttpStatusCode.InternalServerError);
            Assert.Equal(expectedMsg, response.Message);
            Assert.NotNull(response.ErrorUid);
            Assert.NotEmpty(response.Errors);


            response = await this.InvokeMiddlewareAsync(GetOptions(true, overrideMsg), next, HttpStatusCode.InternalServerError);
            Assert.Equal(overrideMsg, response.Message);
            Assert.NotNull(response.ErrorUid);
            Assert.NotEmpty(response.Errors);

            response = await this.InvokeMiddlewareAsync(GetOptions(), next, HttpStatusCode.InternalServerError);
            Assert.Equal(expectedMsg, response.Message);
            Assert.NotNull(response.ErrorUid);
            Assert.Empty(response.Errors);

            response = await this.InvokeMiddlewareAsync(GetOptions(false, overrideMsg), next, HttpStatusCode.InternalServerError);
            Assert.Equal(overrideMsg, response.Message);
            Assert.NotNull(response.ErrorUid);
            Assert.Empty(response.Errors);

        }


        [Fact]
        public async Task OltMiddlewarePayload_OltBadRequestException()
        {
            //arrange
            var expectedException = new OltBadRequestException("Test Bad Request");
            RequestDelegate next = (HttpContext hc) => Task.FromException(expectedException);
            var response = await this.InvokeMiddlewareAsync(GetOptions(true), next, HttpStatusCode.BadRequest);

            Assert.Equal(expectedException.Message, response.Message);
            Assert.NotNull(response.ErrorUid);
            Assert.Empty(response.Errors);
        }

        [Fact]
        public async Task OltMiddlewarePayload_OltValidationException()
        {
            var expectedException = new OltValidationException(new List<IOltValidationError> { new OltValidationError { Message = "Test Validation" } });
            RequestDelegate next = (HttpContext hc) => Task.FromException(expectedException);
            var response = await this.InvokeMiddlewareAsync(GetOptions(true), next, HttpStatusCode.BadRequest);

            Assert.Equal("A validation error has occurred.", response.Message);
            Assert.NotNull(response.ErrorUid);
            Assert.NotEmpty(response.Errors);
            Assert.Collection(response.Errors, item => Assert.Equal("Test Validation", item));
        }

        [Fact]
        public async Task OltMiddlewarePayload_OltRecordNotFoundException()
        {
            var expectedException = new OltRecordNotFoundException("Person");
            RequestDelegate next = (HttpContext hc) => Task.FromException(expectedException);
            var response = await this.InvokeMiddlewareAsync(GetOptions(true), next, HttpStatusCode.BadRequest);
            Assert.Equal(expectedException.Message, response.Message);
            Assert.NotNull(response.ErrorUid);
            Assert.Empty(response.Errors);
        }
        

        [Fact]
        public async Task OltMiddlewarePayload_Completed()
        {
            var dto = UnitTestHelper.CreatePersonDto();
            var json = JsonConvert.SerializeObject(dto);

            RequestDelegate next = (HttpContext hc) =>
            {
                return hc.Response.WriteAsync(json);
            };

            var response = await this.InvokeMiddlewareAsync<PersonDto>(GetOptions(true), next, HttpStatusCode.OK);
            response.Should().Equals(dto);
        }


        [Fact]
        public async Task UseSerilogRequestLogging()
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
                        .AddOltSerilog(configOptions => configOptions.ShowExceptionDetails = true);
                })
                .Configure(app =>
                {
                    app.UseOltSerilogRequestLogging(
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

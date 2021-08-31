////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using Xunit;
////using Microsoft.Extensions.DependencyInjection;
////using Microsoft.AspNetCore.Hosting;
////using Microsoft.AspNetCore.Mvc.Testing;
////using Microsoft.AspNetCore.Builder;
////using OLT.Core;
////using OLT.Libraries.UnitTest.Assets.LocalServices;
////using OLT.Libraries.UnitTest.Assets.Models;
////using OLT.Libraries.UnitTest.Helpers.Factory;
////using Serilog;
////using Serilog.AspNetCore;
////using Serilog.Filters;
////using Xunit;
////using OLT.Logging.Serilog;
////using Serilog.Core;
////using Serilog.Events;

////namespace OLT.Libraries.UnitTest.OLT.Logging.Serilog
////{
////    public class SerilogSink : ILogEventSink
////    {
////        public List<LogEvent> Writes { get; set; } = new List<LogEvent>();

////        public void Emit(LogEvent logEvent)
////        {
////            Writes.Add(logEvent);
////        }
////    }

////    public class WebHostBuilderExtensions : IClassFixture<OltWebApplicationFactory>
////    {
////        private readonly OltWebApplicationFactory _web;

////        public WebHostBuilderExtensions(OltWebApplicationFactory web)
////        {
////            _web = web;
////        }

////        [Fact]
////        public async Task MiddlewareSessionLoggingShouldEnrich()
////        {
////            var (sink, web) = Setup(options =>
////            {
////                options.EnrichDiagnosticContext += (diagnosticContext, httpContext) =>
////                {
////                    diagnosticContext.Set("SomeInteger", 42);
////                };
////            });

////            await web.CreateClient().GetAsync("/resource");
            
////            Assert.NotEmpty(sink.Writes);

////            ////sink.Writes.ForEach(item =>
////            ////{
////            ////    var xy = item.RenderMessage();
////            ////});

////            var completionEvent = sink.Writes.First(logEvent => Matching.FromSource<OltMiddlewareSession>()(logEvent));

////            Assert.Equal(OltUnitTestAppIdentity.StaticEmail, completionEvent.Properties["UserPrincipalName"].ToString());
////            //Assert.Equal("string", completionEvent.Properties["SomeString"].LiteralValue());
////            //Assert.Equal("/resource", completionEvent.Properties["RequestPath"].LiteralValue());
////            //Assert.Equal(200, completionEvent.Properties["StatusCode"].LiteralValue());
////            //Assert.Equal("GET", completionEvent.Properties["RequestMethod"].LiteralValue());
////            //Assert.True(completionEvent.Properties.ContainsKey("Elapsed"));
////        }

////        WebApplicationFactory<OltWebHostTestStartup> Setup(ILogger logger, bool dispose, Action<RequestLoggingOptions> configureOptions = null)
////        {

////            var web = _web.WithWebHostBuilder(
////                builder => builder
////                    .ConfigureServices(services =>
////                        services
////                            //.AddOltAspNetCore(settings, this.GetType().Assembly, null)
////                            .AddOltSerilog()
////                            .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
////                            .Configure<RequestLoggingOptions>(options =>
////                            {
////                                options.Logger = logger;
////                                //options.EnrichDiagnosticContext += (diagnosticContext, httpContext) =>
////                                //{
////                                //    diagnosticContext.Set("SomeString", "string");
////                                //};
////                            }))
////                    .Configure(app =>
////                    {
////                        app.UseSerilogRequestLogging(new OltOptionsAspNetSerilog());
////                        app.Run(_ => Task.CompletedTask); // 200 OK
////                    })
////                    .UseSerilog(logger, dispose));

////            return web;
////        }

////        (SerilogSink, WebApplicationFactory<OltWebHostTestStartup>) Setup(Action<RequestLoggingOptions> configureOptions = null)
////        {
////            var sink = new SerilogSink();
////            var logger = new LoggerConfiguration()
////                .Enrich.FromLogContext()
////                .WriteTo.Sink(sink)
////                .CreateLogger();

////            var web = Setup(logger, true, configureOptions);

////            return (sink, web);
////        }
////    }
////}
////using System;
////using System.Threading.Tasks;
////using Microsoft.AspNetCore.Builder;
////using Microsoft.AspNetCore.Hosting;
////using Microsoft.AspNetCore.Mvc.Testing;
////using Microsoft.Extensions.DependencyInjection;
////using OLT.Core;
////using OLT.Libraries.UnitTest.Assets.LocalServices;
////using OLT.Libraries.UnitTest.Helpers.Factory;
////using OLT.Logging.Serilog;
////using Serilog;
////using Serilog.AspNetCore;
////using Serilog.Filters;
////using Xunit;

////namespace OLT.Libraries.UnitTest.OLT.AspNetCore
////{
////    public class AspNetCoreExtensions : OltBaseAspNetCoreHostFactoryTest
////    {
////        public AspNetCoreExtensions(OltWebApplicationFactory web) : base(web)
////        {
////        }

////        protected override void ConfigureServices(IServiceCollection services)
////        {
////            throw new NotImplementedException();
////        }

////        protected override void Configure(IApplicationBuilder app)
////        {
////            throw new NotImplementedException();
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

////            var completionEvent = sink.Writes.First(logEvent => Matching.FromSource<OltMiddlewareSessionLogging>()(logEvent));

////            Assert.Equal(OltUnitTestAppIdentity.StaticEmail, completionEvent.Properties["UserPrincipalName"].ToString());
////            //Assert.Equal("string", completionEvent.Properties["SomeString"].LiteralValue());
////            //Assert.Equal("/resource", completionEvent.Properties["RequestPath"].LiteralValue());
////            //Assert.Equal(200, completionEvent.Properties["StatusCode"].LiteralValue());
////            //Assert.Equal("GET", completionEvent.Properties["RequestMethod"].LiteralValue());
////            //Assert.True(completionEvent.Properties.ContainsKey("Elapsed"));
////        }
////    }
////}
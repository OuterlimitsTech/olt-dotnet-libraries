////using System.Linq;
////using System.Net;
////using System.Net.Http;
////using System.Net.Http.Headers;
////using System.Text;
////using System.Text.Json;
////using System.Threading.Tasks;
////using OLT.Libraries.UnitTest.Helpers.Factory;
////using Xunit;

////namespace OLT.Libraries.UnitTest.OLT.AspNetCore
////{
////    public class AspNetCoreExtensions : OltBaseAspNetCoreHostFactoryTest
////    {
////        public AspNetCoreExtensions(OltWebApplicationFactory factory) : base(factory)
////        {
////        }

////        //public AppSettingsDto AppSettings
////        //{
////        //    get
////        //    {
////        //        var appSettingsSection = Configuration.GetSection("AppSettings");
////        //        return appSettingsSection.Get<AppSettingsDto>() ?? new AppSettingsDto();
////        //    }
////        //}



////        [Fact]
////        public async Task ConfirmHttpClient()
////        {
////            var request = new HttpRequestMessage(HttpMethod.Post, "api/bogus");

////            var payload = JsonSerializer.Serialize(new
////            {
////                value1 = Faker.Lorem.Sentence(),
////                value2 = Faker.Lorem.Words(10).ToList()
////            });

////            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
////            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "This is an invalid token");

////            var response = await HttpClient.SendAsync(request);

////            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
////        }

////        //[Fact]
////        //public async Task MiddlewareSessionLoggingShouldEnrich()
////        //{
////        //    var test = new Microsoft.AspNetCore.Http.htt
////        //    //var request = new HttpRequestMessage(HttpMethod.Get, "api/bogus?param1=1&param2=0");

////        //    //var response = await HttpClient.SendAsync(request);
////        //    //var (sink, web) = Setup(options =>
////        //    //{
////        //    //    options.EnrichDiagnosticContext += (diagnosticContext, httpContext) =>
////        //    //    {
////        //    //        diagnosticContext.Set("SomeInteger", 42);
////        //    //    };
////        //    //});




////        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
////        //}



////        //[Fact]
////        //public async Task MiddlewareSessionLoggingShouldEnrich()
////        //{
////        //    var (sink, web) = Setup(options =>
////        //    {
////        //        options.EnrichDiagnosticContext += (diagnosticContext, httpContext) =>
////        //        {
////        //            diagnosticContext.Set("SomeInteger", 42);
////        //        };
////        //    });

////        //    await web.CreateClient().GetAsync("/resource");

////        //    Assert.NotEmpty(sink.Writes);

////        //    var completionEvent = sink.Writes.First(logEvent => Matching.FromSource<OltMiddlewareSessionLogging>()(logEvent));

////        //    Assert.Equal(OltUnitTestAppIdentity.StaticEmail, completionEvent.Properties["UserPrincipalName"].ToString());
////        //    //Assert.Equal("string", completionEvent.Properties["SomeString"].LiteralValue());
////        //    //Assert.Equal("/resource", completionEvent.Properties["RequestPath"].LiteralValue());
////        //    //Assert.Equal(200, completionEvent.Properties["StatusCode"].LiteralValue());
////        //    //Assert.Equal("GET", completionEvent.Properties["RequestMethod"].LiteralValue());
////        //    //Assert.True(completionEvent.Properties.ContainsKey("Elapsed"));
////        //}
////    }
////}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OLT.Libraries.UnitTest.Helpers.Factory
{
    public class OltWebApplicationFactory : WebApplicationFactory<OltWebHostTestStartup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder() => new WebHostBuilder().UseStartup<OltWebHostTestStartup>();
        protected override void ConfigureWebHost(IWebHostBuilder builder) => builder.UseContentRoot(".");
    }
}
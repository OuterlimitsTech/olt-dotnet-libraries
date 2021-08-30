using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace OLT.Libraries.UnitTest.Helpers.Factory
{
    public abstract class OltBaseAspNetCoreHostFactoryTest : IClassFixture<OltWebApplicationFactory>
    {

        protected OltBaseAspNetCoreHostFactoryTest(OltWebApplicationFactory web)
        {
            Web = web;
        }

        protected OltWebApplicationFactory Web { get; }

        protected abstract void ConfigureServices(IServiceCollection services);
        protected abstract void Configure(IApplicationBuilder app);

        WebApplicationFactory<OltWebHostTestStartup> Setup(ILogger logger, bool dispose)
        {

            var web = Web.WithWebHostBuilder(
                builder => builder
                    .ConfigureServices(ConfigureServices)
                    .Configure(app =>
                    {
                        Configure(app);
                        app.Run(_ => Task.CompletedTask); // 200 OK
                    }));


            return web;
        }
    }
}
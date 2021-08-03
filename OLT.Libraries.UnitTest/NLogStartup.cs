using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using OLT.AspNetCore.NLog;
using OLT.Core;

namespace OLT.Libraries.UnitTest
{
    // ReSharper disable once InconsistentNaming
    public class NLogStartup : Startup
    {

        public override void ConfigureServices(IServiceCollection services)
        {
            //GlobalDiagnosticsContext.Set("connectionString", service);

            base.ConfigureServices(services);
            //services.AddControllers().AddApplicationPart(Assembly.Load("RoundTheCode.CrudApi.Web")).AddControllersAsServices();
        }


        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOltDefaults(Settings, () => app.UseOltNLogRequestLogging(Settings));
        }
    }
}
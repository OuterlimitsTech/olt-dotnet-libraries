using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Logging;
using OLT.Core;
using Serilog;

namespace OLT.Libraries.UnitTest
{

    // ReSharper disable once InconsistentNaming
    public class SerilogStartup : Startup
    {
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOltDefaults(Settings, () => app.UseSerilogRequestLogging());
        }
    }
}
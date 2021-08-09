using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Models;
using Serilog;

namespace OLT.Libraries.UnitTest
{

    // ReSharper disable once InconsistentNaming
    public class SerilogStartup : Startup
    {
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsDto> options)
        {
            app.UseOltDefaults(options.Value, () => app.UseSerilogRequestLogging());
        }
    }
}
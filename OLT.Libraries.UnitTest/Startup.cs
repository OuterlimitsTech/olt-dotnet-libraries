using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Extensions;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest
{
    // ReSharper disable once InconsistentNaming
    public class Startup
    {
        protected readonly OltAspNetAppSettings Settings = new OltAspNetAppSettings
        {
            Hosting = new OltAspNetHostingAppSettings
            {
                CorsPolicyName = OltAspNetDefaults.CorsPolicyName,
                ShowExceptionDetails = true,
                ConfigurationName = OltAspNetDefaults.HostingConfigurations.Default
            }
        };

        public virtual void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettingsDto>(appSettingsSection);

            services.AddOltUnitTesting(() =>
            {
                services.AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                    optionsBuilder.UseInMemoryDatabase(databaseName: "Test"));

                services.AddControllers();
            });


        }

        
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseOltDefaults(Settings, (builder, settings) => builder.UseOltNLogRequestLogging(settings));
        }
    }

    
}

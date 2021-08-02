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
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity;
using OLT.Libraries.UnitTest.Assests.Extensions;

namespace OLT.Libraries.UnitTest
{
    // ReSharper disable once InconsistentNaming
    public class Startup
    {
        protected readonly OltAspNetAppSettings Settings = new OltAspNetAppSettings
        {
            CorsPolicyName = OltAspNetDefaults.CorsPolicyName,
            ShowExceptionDetails = true,
            Hosting = new OltAspNetHostingAppSettings
            {
                ConfigurationName = OltAspNetDefaults.HostingConfigurations.Default
            }
        };

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddOltUnitTesting(new OltInjectionOptions());
            var optionsBuilder = new DbContextOptionsBuilder<SqlDatabaseContext>().UseInMemoryDatabase(databaseName: "Test");
            services.AddOltSqlServer(optionsBuilder, (options, logService, auditUser) => new SqlDatabaseContext(options, logService, auditUser));
            services.AddControllers();
        }

        
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseOltDefaults(Settings, (builder, settings) => builder.UseOltNLogRequestLogging(settings));
        }
    }

    
}

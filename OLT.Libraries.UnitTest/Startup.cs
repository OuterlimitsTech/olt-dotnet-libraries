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
using Microsoft.Extensions.Options;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Extensions;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest
{
    
    // ReSharper disable once InconsistentNaming
    public class Startup
    {

        
        protected IConfiguration BuildConfiguration(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true);


            var configuration = builder.Build();

            services.AddSingleton<IConfiguration>(configuration);

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettingsDto>(appSettingsSection);

            return configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            var configuration = BuildConfiguration(services);

            services
                .AddOltUnitTesting()
                .AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                {
                    optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
                })
                .AddControllers();
        }


        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsDto> options)
        {
            
        }
    }

    
}

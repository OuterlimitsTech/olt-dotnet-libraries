using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Extensions;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Email;
using OLT.Email.SendGrid;
using OLT.Libraries.UnitTest.Assets.Email.SendGrid;

namespace OLT.Libraries.UnitTest
{
   

    // ReSharper disable once InconsistentNaming
    public class Startup
    {
      
        public static void LoadLocalEnvironmentVariables()
        {
            using var file = File.OpenText("Properties\\launchSettings.json");
            var reader = new JsonTextReader(file);
            var jObject = JObject.Load(reader);

            var variables = jObject
                .GetValue("profiles")
                //select a proper profile here
                .SelectMany(profiles => profiles.Children())
                .SelectMany(profile => profile.Children<JProperty>())
                .Where(prop => prop.Name == "environmentVariables")
                .SelectMany(prop => prop.Value.Children<JProperty>())
                .ToList();

            foreach (var variable in variables)
            {
                Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
            }
        }

        // custom host build
        public void ConfigureHost(IHostBuilder hostBuilder)
        {

#if DEBUG
            LoadLocalEnvironmentVariables();
#endif

            hostBuilder.ConfigureHostConfiguration(builder =>
            {
                builder
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables()
                    .AddUserSecrets<Startup>();
            });
        }


        public virtual void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            var appSettingsSection = hostBuilderContext.Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsDto>(appSettingsSection);
            var settings = appSettingsSection.Get<AppSettingsDto>();
            var sendGridSettings = settings.SendGrid;
            

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.Configure<OltAppSettingsSendGrid>(opt =>
            {
                opt.From = new OltEmailAddress
                {
                    Name = sendGridSettings.From.Name,
                    Email = sendGridSettings.From.Email,
                };
                opt.TestWhitelist = new OltEmailConfigurationWhitelist
                {
                    Domain = sendGridSettings.TestWhitelist.Domain,
                    Email = sendGridSettings.TestWhitelist.Email
                };
                opt.Production = sendGridSettings.Production;
                opt.ApiKey = hostBuilderContext.Configuration.GetValue<string>("SENDGRID_TOKEN") ?? Environment.GetEnvironmentVariable("SENDGRID_TOKEN");
            });


            services
                .AddOltUnitTesting(this.GetType().Assembly)
                .AddScoped<IOltEmailService, OltSendGridEmailService>()
                .AddScoped<IOltEmailConfigurationSendGrid, EmailApiConfiguration>()
                //.AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                //{
                //    optionsBuilder.UseInMemoryDatabase(databaseName: "test-db-1");
                //})
                //.AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                //{
                //    optionsBuilder.UseInMemoryDatabase(databaseName: "test-db-2");
                //})

                .AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                {
                    //optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=test-db-1;Integrated Security=True;");
                    optionsBuilder.UseLazyLoadingProxies(false);

                    var options = optionsBuilder.Options as DbContextOptions<SqlDatabaseContext>;
                    using (var context = new SqlDatabaseContext(options))
                    {
                        context.Database.EnsureDeleted();
                    }
                    using (var context = new SqlDatabaseContext(options))
                    {
                        context.Database.EnsureCreated();
                    }
                })
                .AddDbContextPool<SqlDatabaseContext2>((serviceProvider, optionsBuilder) =>
                {

                    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=test-db-2;Integrated Security=True;");
                    optionsBuilder.UseLazyLoadingProxies(false);

                    var options = optionsBuilder.Options as DbContextOptions<SqlDatabaseContext2>;
                    using (var context = new SqlDatabaseContext2(options))
                    {
                        context.Database.EnsureDeleted();
                    }
                    using (var context = new SqlDatabaseContext2(options))
                    {
                        context.Database.EnsureCreated();
                    }
                })
                .AddControllers();
        }


        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsDto> options)
        {

        }
    }

    
}

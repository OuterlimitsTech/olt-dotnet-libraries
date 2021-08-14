using Microsoft.Extensions.DependencyInjection;
using System;
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
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                    .AddUserSecrets<Startup>()
                    .AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables();
            });
        }


        public virtual void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            var appSettingsSection = hostBuilderContext.Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsDto>(appSettingsSection);
            var settings = appSettingsSection.Get<AppSettingsDto>();

            var sendGridSettings =
                hostBuilderContext.Configuration.GetSection("AppSettings:SendGrid").Get<OltSendGridAppSettings>();
            var x = sendGridSettings.FromEmail;

            services.Configure<OltSendGridAppSettings>(settings =>
            {
                settings.FromEmail = sendGridSettings.FromEmail;
                settings.FromName = sendGridSettings.FromName;
                settings.Production = sendGridSettings.Production;
                settings.DomainWhitelist = sendGridSettings.DomainWhitelist;
                settings.EmailWhitelist = sendGridSettings.EmailWhitelist;
                settings.ApiKey = hostBuilderContext.Configuration.GetValue<string>("SENDGRID_TOKEN") ?? Environment.GetEnvironmentVariable("SENDGRID_TOKEN");
            });

            

            
            //sendGridSettings.ApiKey = ;

            //services.AddAuthentication(new OltAuthenticationJwtBearer(settings.JwtSecret), null);

            services
                .AddOltUnitTesting()
                .AddScoped<IOltEmailService, OltSendGridEmailService>()
                .AddScoped<IOltEmailConfigurationSendGrid, EmailApiConfiguration>()
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

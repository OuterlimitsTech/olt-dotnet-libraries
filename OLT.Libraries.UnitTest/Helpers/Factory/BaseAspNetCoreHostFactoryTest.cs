using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using OLT.Core;
using OLT.Email;
using OLT.Email.SendGrid;
using OLT.Libraries.UnitTest.Assets.Email.SendGrid;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Extensions;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Logging.Serilog;
using Serilog;
using Xunit;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace OLT.Libraries.UnitTest.Helpers.Factory
{
    public static class SerilogExtensions
    {

        public static LoggerConfiguration AppConfigureSeriLog(this LoggerConfiguration loggerConfiguration, IConfiguration configuration, string environmentName, string connectionString, string tableName = "Log")
        {
            return loggerConfiguration;
        }
    }



    public abstract class OltBaseAspNetCoreHostFactoryTest : IClassFixture<OltWebApplicationFactory>
    {

        protected OltBaseAspNetCoreHostFactoryTest(OltWebApplicationFactory factory)
        {
            Factory = factory;
            HttpClient = factory.WithWebHostBuilder(
                builder => builder
                    .ConfigureServices((context, services) => ConfigureServices(services, context.Configuration))
                    .UseSerilog((hostingContext, configuration) =>
                    {
                        configuration.AppConfigureSeriLog(hostingContext.Configuration, hostingContext.HostingEnvironment.EnvironmentName, hostingContext.Configuration.GetOltConnectionString("DbConnection"));
                    })
                    .Configure((context, app) =>
                    {
                        Configure(app, context.HostingEnvironment, context.Configuration);
                        app.Run(_ => Task.CompletedTask); // 200 OK
                    })).CreateClient();

        }

        protected OltWebApplicationFactory Factory { get; }
        protected  HttpClient HttpClient { get; }

        //protected abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);
        //protected abstract void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration);


        protected virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings")?.Get<AppSettingsDto>() ?? new AppSettingsDto();

            services
                .AddOltUnitTesting(this.GetType().Assembly)
                .AddOltAspNetCore(appSettings, this.GetType().Assembly, mvcBuilder => mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter())))
                .AddOltSerilog()
                .AddOltAddMemoryCache()
                .AddScoped<IOltEmailService, OltSendGridEmailService>()
                .AddScoped<IOltEmailConfigurationSendGrid, EmailApiConfiguration>()
                .AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                {
                    optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                })
                .AddControllers();
        }

        protected virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings")?.Get<AppSettingsDto>() ?? new AppSettingsDto();

            app.UsePathBase(appSettings.Hosting);
            app.UseDeveloperExceptionPage(appSettings.Hosting);
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                await next();
            });
            app.UseHsts(appSettings.Hosting);
            app.UseCors(appSettings.Hosting);
            app.UseHttpsRedirection(appSettings.Hosting);
            app.UseAuthentication();
            app.UseSerilogRequestLogging(new OltOptionsAspNetSerilog { DisableMiddlewareRegistration = false });
            app.UseSwaggerWithUI(appSettings.Swagger);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

     
    }
}
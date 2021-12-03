using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Logging.Serilog;
using Serilog;

namespace OLT.Libraries.UnitTest
{
    public class TestServerStartup
    {
        private readonly IConfiguration _configuration;

        public TestServerStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var settings = _configuration.GetSection("AppSettings").Get<AppSettingsDto>();
            var jwtSecret = Guid.NewGuid().ToString().Replace("-", string.Empty);

            services
                .AddOltAspNetCore(settings, this.GetType().Assembly, null)
                .AddOltInjectionAutoMapper()
                .AddScoped<IOltIdentity, OltUnitTestNullIdentity>()
                .AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                {
                    optionsBuilder.UseInMemoryDatabase(databaseName: $"TestServer_{Guid.NewGuid()}");
                })
                .AddAuthentication(new OltAuthenticationJwtBearer(jwtSecret));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsDto> options)
        {
            var settings = options.Value;
            app.UsePathBase(settings.Hosting);
            app.UseDeveloperExceptionPage(settings.Hosting);
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHsts(settings.Hosting);
            app.UseCors(settings.Hosting);
            app.UseHttpsRedirection(settings.Hosting);
            app.UseSwaggerWithUI(settings.Swagger);
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }

    // ReSharper disable once InconsistentNaming
    public class SerilogStartup
    {
        private readonly IConfiguration _configuration;

        public SerilogStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var settings = _configuration.GetSection("AppSettings").Get<AppSettingsDto>();
            var jwtSecret = Guid.NewGuid().ToString().Replace("-", string.Empty);

            services
                .AddOltAspNetCore(settings, this.GetType().Assembly, null)
                .AddOltInjectionAutoMapper()
                .AddOltSerilog(configOptions => configOptions.ShowExceptionDetails = true)
                .AddScoped<IOltIdentity, OltUnitTestNullIdentity>()
                .AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                {
                    optionsBuilder.UseInMemoryDatabase(databaseName: $"{nameof(SerilogStartup)}_{Guid.NewGuid()}");
                })
                .AddAuthentication(new OltAuthenticationJwtBearer(jwtSecret));

            // services.AddControllers().AddApplicationPart(Assembly.Load("RoundTheCode.CrudApi.Web")).AddControllersAsServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsDto> options)
        {
            var settings = options.Value;
            app.UsePathBase(settings.Hosting);
            app.UseDeveloperExceptionPage(settings.Hosting);
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHsts(settings.Hosting);
            app.UseCors(settings.Hosting);
            app.UseHttpsRedirection(settings.Hosting);
            //app.UseAuthentication();
            app.UseOltSerilogRequestLogging();
            app.UseSwaggerWithUI(settings.Swagger);
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
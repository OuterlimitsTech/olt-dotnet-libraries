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

            //services.AddAuthentication(new OltAuthenticationJwtBearer(settings.JwtSecret), null);

            services
                .AddOltAspNetCore(settings, this.GetType().Assembly, null)
                .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
                .AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
                {
                    optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
                });

            //var key = Encoding.ASCII.GetBytes(settings.JwtSecret);
            //services
            //    .AddAuthentication(opt =>
            //    {
            //        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
            //        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            //        //configureOptions?.Invoke(opt);
            //    }).AddJwtBearer(opt =>
            //    {
            //        opt.RequireHttpsMetadata = false;
            //        opt.SaveToken = true;
            //        opt.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(key),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };

            //        //configureOptions?.Invoke(opt);
            //    });

            // services.AddControllers().AddApplicationPart(Assembly.Load("RoundTheCode.CrudApi.Web")).AddControllersAsServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsDto> options)
        {
            var user = app.ApplicationServices.GetRequiredService<IOltIdentity>();

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
            //app.UseSerilogRequestLogging(new OltOptionsSerilog());
            app.UseSwaggerWithUI(settings.Swagger);
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
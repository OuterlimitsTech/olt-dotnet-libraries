using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using System;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication.JwtToken
{
    public class JwtTokenStartup
    {
        private readonly IConfiguration _configuration;

        public const string Secret = "TopSecretValue";

        public JwtTokenStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var settings = _configuration.GetSection("AppSettings").Get<AppSettingsDto>();            

            services
                .AddOltAspNetCore(settings, this.GetType().Assembly, null)                
                .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
                .AddAuthentication(new OltAuthenticationJwtBearer(Secret));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsDto> options)
        {
            var settings = options.Value;
            app.UsePathBase(settings.Hosting);
            app.UseDeveloperExceptionPage(settings.Hosting);
            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            //});
            //app.UseHsts(settings.Hosting);
            app.UseCors(settings.Hosting);
            app.UseHttpsRedirection(settings.Hosting);
            app.UseAuthentication();
            //app.UseOltSerilogRequestLogging();
            //app.UseSwaggerWithUI(settings.Swagger);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

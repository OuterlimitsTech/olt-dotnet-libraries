using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Extensions;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication.JwtToken
{

    public static class JwtTokenTestExts
    {
        public const string Authority = "local_Authority";
        public const string Audience = "local_Audience";

        public static OltAuthenticationJwtBearer GetOptions()
        {
            return new OltAuthenticationJwtBearer
            {
                JwtSecret = "ABC1234",
                RequireHttpsMetadata = false,
                ValidateIssuer = true,
                ValidateAudience = true,
            };
        }
    }
    public abstract class BaseJwtTokenStartup
    {
        public BaseJwtTokenStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

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
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public void DefaultServices(IServiceCollection services)
        {
            var settings = Configuration.GetSection("AppSettings").Get<AppSettingsDto>();

            services
                .AddOltUnitTesting(this.GetType().Assembly)
                .AddOltAspNetCore(settings, this.GetType().Assembly, null);
        }
    }


    public class JwtTokenStartupDefault : BaseJwtTokenStartup
    {
        public JwtTokenStartupDefault(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            base.DefaultServices(services);
            services.AddJwtBearer(JwtTokenTestExts.GetOptions(), opts =>
            {
                opts.Authority = JwtTokenTestExts.Authority;
                opts.Audience = JwtTokenTestExts.Audience;
            });
        }
    }
}

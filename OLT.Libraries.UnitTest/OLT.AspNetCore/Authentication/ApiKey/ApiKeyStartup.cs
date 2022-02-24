using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Extensions;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using AspNetCore.Authentication.ApiKey;


namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication.ApiKey
{
    public static class ApiKeyConstants
    {
        public const string Key1 = "1234";
        public const string Realm = "API Key Unit Test";
    }

    public abstract class BaseApiKeyStartup
    {
        public BaseApiKeyStartup(IConfiguration configuration)
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
                .AddOltAspNetCore(settings, this.GetType().Assembly, null)
                .AddScoped<IOltIdentity, OltUnitTestAppIdentity>()
                .AddScoped<IOltApiKeyProvider, ApiKeyProvider>()
                .AddScoped<IApiKeyProvider>(opt => opt.GetRequiredService<IOltApiKeyProvider>())
                .AddScoped<ApiKeyService>()
                .AddScoped<IOltApiKeyService>(opt => opt.GetRequiredService<ApiKeyService>());
        }
    }

   
    public class ApiKeyStartupDefault : BaseApiKeyStartup
    {
        public ApiKeyStartupDefault(IConfiguration configuration) : base(configuration)  { }

        public void ConfigureServices(IServiceCollection services)
        {
            base.DefaultServices(services);
            services.AddApiKey(new OltAuthenticationApiKey<ApiKeyProvider>(ApiKeyConstants.Realm));
        }
    }

    public class ApiKeyStartupQueryParamsOnly : BaseApiKeyStartup
    {
        public ApiKeyStartupQueryParamsOnly(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            base.DefaultServices(services);
            services.AddApiKey(new OltAuthenticationApiKey<ApiKeyProvider>(ApiKeyConstants.Realm, OltApiKeyLocation.QueryParamsOnly));
        }
    }

    public class ApiKeyStartupQueryParamsOnlyWithOptions : BaseApiKeyStartup
    {
        public ApiKeyStartupQueryParamsOnlyWithOptions(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            base.DefaultServices(services);
            services.AddApiKey(new OltAuthenticationApiKey<ApiKeyProvider>(ApiKeyConstants.Realm, OltApiKeyLocation.QueryParamsOnly), opt => opt.IgnoreAuthenticationIfAllowAnonymous = false);
        }
    }


    public class ApiKeyStartupHeaderOnly : BaseApiKeyStartup
    {
        public ApiKeyStartupHeaderOnly(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            base.DefaultServices(services);
            services.AddApiKey(new OltAuthenticationApiKey<ApiKeyProvider>(ApiKeyConstants.Realm, OltApiKeyLocation.HeaderOnly));
        }
    }

    public class ApiKeyStartupHeaderOnlyWithOptions : BaseApiKeyStartup
    {
        public ApiKeyStartupHeaderOnlyWithOptions(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            base.DefaultServices(services);
            services.AddApiKey(new OltAuthenticationApiKey<ApiKeyProvider>(ApiKeyConstants.Realm, OltApiKeyLocation.HeaderOnly), opt => opt.IgnoreAuthenticationIfAllowAnonymous = false);
        }
    }

    public class ApiKeyStartupHeaderOrQueryParams : BaseApiKeyStartup
    {
        public ApiKeyStartupHeaderOrQueryParams(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            base.DefaultServices(services);
            services.AddApiKey(new OltAuthenticationApiKey<ApiKeyProvider>(ApiKeyConstants.Realm, OltApiKeyLocation.HeaderOrQueryParams));
        }
    }


    public class ApiKeyStartupHeaderOrQueryParamsWithOptions : BaseApiKeyStartup
    {
        public ApiKeyStartupHeaderOrQueryParamsWithOptions(IConfiguration configuration) : base(configuration) { }

        public void ConfigureServices(IServiceCollection services)
        {
            base.DefaultServices(services);
            services.AddApiKey(new OltAuthenticationApiKey<ApiKeyProvider>(ApiKeyConstants.Realm, OltApiKeyLocation.HeaderOrQueryParams), opt => opt.IgnoreAuthenticationIfAllowAnonymous = false);
        }
    }
}

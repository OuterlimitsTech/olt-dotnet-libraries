//using System;
//using System.Reflection;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Options;
//using OLT.AspNetCore.Authentication;
//using OLT.Core;
//using OLT.Libraries.UnitTest.Assets.ApiKey;
//using OLT.Libraries.UnitTest.Assets.Entity;
//using OLT.Libraries.UnitTest.Assets.Extensions;
//using OLT.Libraries.UnitTest.Assets.Models;
//using OLT.Logging.NLog;

//namespace OLT.Libraries.UnitTest
//{
//    // ReSharper disable once InconsistentNaming
//    public class NLogStartup
//    {
//        private readonly IConfiguration _configuration;

//        public NLogStartup(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            var settings = _configuration.GetSection("AppSettings").Get<AppSettingsDto>();

//            services
//                .AddOltAspNetCore(settings, this.GetType().Assembly, null)
//                .AddScoped<IOltApiKeyService, ApiKeyService>()
//                .AddDbContextPool<SqlDatabaseContext>((serviceProvider, optionsBuilder) =>
//                {
//                    optionsBuilder.UseInMemoryDatabase(databaseName: "Test");
//                })
//                .AddAuthentication(new OltAuthenticationApiKey<OltApiKeyProvider<ApiKeyService>>("Unit Test"), null);

//            // services.AddControllers().AddApplicationPart(Assembly.Load("RoundTheCode.CrudApi.Web")).AddControllersAsServices();
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettingsDto> options)
//        {
//            //var x = "1234";
//            //app.UseOltDefaults(options.Value, () => app.UseOltNLogExceptionLogging(options.Value.Hosting.ShowExceptionDetails));
//        }
//    }
//}
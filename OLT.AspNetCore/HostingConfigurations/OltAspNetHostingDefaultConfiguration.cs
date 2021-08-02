using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;

namespace OLT.Core
{
    public class OltAspNetHostingDefaultConfiguration : OltAspNetHostingBaseConfiguration
    {
        public override string Name => OltAspNetDefaults.HostingConfigurations.Default;


        public override IApplicationBuilder Configure<TSettings>(IApplicationBuilder app, TSettings settings, Action<IApplicationBuilder, TSettings> middlewareLogging)
        {

            if (settings.Hosting.PathBase.IsNotEmpty())
            {
                app.UsePathBase(settings.Hosting.PathBase);
            }

            if (settings.ShowExceptionDetails)
            {
                app.UseDeveloperExceptionPage();
            }

            middlewareLogging?.Invoke(app, settings);

            if (settings.Hosting.UseHsts)
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            settings.Swagger.Apply(app);


            if (settings.CorsPolicyName.IsNotEmpty())
            {
                app.UseCors(settings.CorsPolicyName);
            }

            if (settings.Hosting.DisableHttpsRedirect == false)
            {
                app.UseHttpsRedirection();
            }
            
            app.UseRouting();

            if (settings.Hosting.DisableUseAuthentication == false)
            {
                app.UseAuthentication();
            }

            if (settings.Hosting.DisableUseAuthorization == false)
            {
                app.UseAuthorization();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
            
        }
    }
}
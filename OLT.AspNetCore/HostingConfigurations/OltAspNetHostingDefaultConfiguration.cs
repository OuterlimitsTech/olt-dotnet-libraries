using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltAspNetHostingDefaultConfiguration : OltAspNetHostingBaseConfiguration
    {
        public override string Name => OltAspNetDefaults.HostingConfigurations.Default;


        public override IApplicationBuilder Configure<TSettings>(IApplicationBuilder app, TSettings settings, Action action)
        {

            if (settings.Hosting.PathBase.IsNotEmpty())
            {
                app.UsePathBase(settings.Hosting.PathBase);
            }

            if (settings.Hosting.ShowExceptionDetails)
            {
                app.UseDeveloperExceptionPage();
            }

            action?.Invoke();

            if (settings.Hosting.UseHsts)
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            if (settings.Swagger.Enabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                    // Add a swagger UI for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        var deprecated = description.IsDeprecated ? " DEPRECATED" : "";
                        c.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"{settings.Swagger.Title} API {description.GroupName}{deprecated}");
                    }
                });
            }

            if (settings.Hosting.CorsPolicyName.IsNotEmpty())
            {
                app.UseCors(settings.Hosting.CorsPolicyName);
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
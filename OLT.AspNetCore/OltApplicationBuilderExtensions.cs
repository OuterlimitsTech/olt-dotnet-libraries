using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OLT.Core
{
    public static class OltApplicationBuilderExtensions
    {

        /// <summary>
        /// Checks if debugger is currently attached
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>Returns true if debugger attached</returns>
        public static bool InDebug(this IHostEnvironment hostEnvironment)
        {
            var inDebug = false;
#if DEBUG
            inDebug = true;
#endif
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return inDebug;
        }

        /// <summary>
        /// Checks if the current host environment name is <see cref="OltDefaults.OltEnvironments.Test"/>.
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>True if the environment name is <see cref="OltDefaults.OltEnvironments.Test"/>, otherwise false.</returns>
        public static bool IsTest(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment(OltDefaults.OltEnvironments.Test);
        }

        public static IApplicationBuilder UseOltDefaults(this IApplicationBuilder app)
        {
            return UseOltDefaults(app, new OltAspNetApplicationOptions());
        }

        public static IApplicationBuilder UseOltDefaults(this IApplicationBuilder app, OltAspNetApplicationOptions options)
        {

            if (options.PathBase.IsNotEmpty())
            {
                app.UsePathBase(options.PathBase);
            }

            if (options.EnableDeveloperExceptionPage)
            {
                app.UseDeveloperExceptionPage();
            }

            if (options.EnableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                    // Add a swagger UI for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        var deprecated = description.IsDeprecated ? " DEPRECATED" : "";
                        c.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"{options.SwaggerAppName} API {description.GroupName}{deprecated}");
                    }
                });
            }

            // needed for NLog ${aspnet-request-posted-body} with an API Controller. Must be before app.UseEndpoints
            app.Use(async (context, next) => {
                context.Request.EnableBuffering();
                await next();
            });

            if (options.Cors.UseCors)
            {
                app.UseCors(options.Cors.Policy.PolicyName ?? new OltAspNetCoreCorsPolicyDisabled().PolicyName);
            }
            

            if (options.EnableBuffering)
            {
                // needed for NLog ${aspnet-request-posted-body} with an API Controller. Must be before app.UseEndpoints
                app.Use(async (context, next) => {
                    context.Request.EnableBuffering();
                    await next();
                });
            }

            if (options.DisableHttpsRedirect == false)
            {
                app.UseHttpsRedirection();
            }

            app.UseOltExceptionHandler(options.ShowErrorDetails, options.SupportEmail);

            app.UseRouting();

            if (options.DisableUseAuthentication == false)
            {
                app.UseAuthentication();
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            return app;
        }

        
    }
}
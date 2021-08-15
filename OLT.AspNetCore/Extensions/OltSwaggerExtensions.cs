using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace OLT.Core
{
    public static partial class OltSwaggerExtensions
    {
        /// <summary>
        /// Adds Swagger Configuration
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"><seealso cref="IServiceCollection"/></param>
        /// <param name="options"><see cref="IOltOptionsAspNetSwaggerUI"/></param>
        /// <param name="setupAction"><see cref="SwaggerGenOptions"/></param>
        /// <returns><seealso cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSwaggerGen<TOptions>(this IServiceCollection services, TOptions options, Action<SwaggerGenOptions> setupAction = null)
            where TOptions : IOltOptionsAspNetSwaggerUI
        {
            if (!options.Enabled)
            {
                return services;
            }
            

            return services
                .AddSwaggerGen(
                    opt =>
                    {
                        // Resolve the temp IApiVersionDescriptionProvider service  
                        var provider = services.BuildServiceProvider()
                            .GetRequiredService<IApiVersionDescriptionProvider>();

                        // Add a swagger document for each discovered API version  
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            opt.SwaggerDoc(description.GroupName, new OpenApiInfo
                            {
                                Title = $"{options.Title}  {description.ApiVersion}",
                                Version = description.ApiVersion.ToString(),
                                Description = description.IsDeprecated ? $"{options.Description} - DEPRECATED" : options.Description,
                            });
                        }

                        // Add a custom filter for setting the default values  
                        opt.OperationFilter<OltSwaggerDefaultValues>();

                        opt.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT",
                            Description = "JWT Authorization header using the Bearer scheme."
                        });

                        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme, 
                                        Id = "bearerAuth"
                                    }
                                },
                                Array.Empty<string>()
                            }
                        });

                        if (options.XmlSettings.CommentsFilePath.IsNotEmpty())
                        {
                            opt.IncludeXmlComments(options.XmlSettings.CommentsFilePath, options.XmlSettings.IncludeControllerXmlComments);
                        }

                        setupAction?.Invoke(opt);
                    });
        }


        /// <summary>
        /// Registers Swagger middleware <seealso cref="SwaggerBuilderExtensions"/> using <seealso cref="IOltOptionsAspNetSwagger.Enabled"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSwagger<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetSwagger
        {

            if (options.Enabled)
            {
                app.UseSwagger();
            }

            return app;
        }

        /// <summary>
        /// Registers SwaggerUI middleware <seealso cref="SwaggerUIBuilderExtensions"/> using <seealso cref="IOltOptionsAspNetSwagger"/>
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSwaggerUI<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetSwaggerUI
        {

            if (options.Enabled)
            {
                app.UseSwaggerUI(c =>
                {
                    var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                    // Add a swagger UI for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        var deprecated = description.IsDeprecated ? " DEPRECATED" : string.Empty;
                        c.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"{options.Title} API {description.GroupName}{deprecated}");
                    }
                });
            }

            return app;
        }

        /// <summary>
        /// Registers Swagger and SwaggerUI middlewares <seealso cref="SwaggerBuilderExtensions"/> and <seealso cref="SwaggerUIBuilderExtensions"/> using <seealso cref="IOltOptionsAspNetSwagger"/>
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSwaggerWithUI<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetSwagger, IOltOptionsAspNetSwaggerUI
        {
            return app
                .UseSwagger(options)
                .UseSwaggerUI(options);
        }


    }
}

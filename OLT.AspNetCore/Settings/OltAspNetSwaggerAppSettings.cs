using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace OLT.Core
{

    public class OltAspNetSwaggerAppSettings : IOltAppSettings
    {
        /// <summary>
        /// Enable Swagger Document
        /// </summary>
        /// <remarks>
        /// In most cases, this should only be enabled in the Development environment. 
        /// </remarks>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// Title used for Swagger.  This will default to the AssemblyProductAttribute name of Assembly
        /// </summary>
        /// <remarks>Default is Web Api</remarks>
        public virtual string Title { get; set; } =
            Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ??
            Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ??
            "Web Api";

        /// <summary>
        /// Title used for Swagger.  This will default to the AssemblyDescriptionAttribute name of Assembly 
        /// </summary>
        /// <remarks>Default is Api Methods</remarks>
        public virtual string Description { get; set; } =
            Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description ??
            Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description ??
            "Api Methods";


        public IServiceCollection Apply(IServiceCollection services)
        {
            if (!Enabled)
            {
                return services;
            }

            return services
                .AddSwaggerGen(
                    options =>
                    {
                        // Resolve the temp IApiVersionDescriptionProvider service  
                        var provider = services.BuildServiceProvider()
                            .GetRequiredService<IApiVersionDescriptionProvider>();

                        // Add a swagger document for each discovered API version  
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerDoc(description.GroupName, new OpenApiInfo
                            {
                                Title = $"{Title}  {description.ApiVersion}",
                                Version = description.ApiVersion.ToString(),
                                Description = description.IsDeprecated ? $"{Description} - DEPRECATED" : Description,
                            });
                        }

                        // Add a custom filter for setting the default values  
                        options.OperationFilter<OltSwaggerDefaultValues>();

                        options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT",
                            Description = "JWT Authorization header using the Bearer scheme."
                        });

                        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                        {Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                                },
                                new string[] { }
                            }
                        });

                        //REMOVED - NOT USING 2020-02-02 Chris
                        // Tells swagger to pick up the output XML document file  
                        //options.IncludeXmlComments(Path.Combine(
                        //    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{this.GetType().Assembly.GetName().Name}.xml"
                        //));

                    });

        }
        public IApplicationBuilder Apply(IApplicationBuilder app)
        {
            if (Enabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                    // Add a swagger UI for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        var deprecated = description.IsDeprecated ? " DEPRECATED" : "";
                        c.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"{Title} API {description.GroupName}{deprecated}");
                    }
                });
            }

            return app;
        }

    }



}
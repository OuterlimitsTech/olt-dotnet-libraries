using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace OLT.Core
{
    public static class OltServiceCollectionExtensions
    {

        /// <summary>
        /// Build Default AspNetCore Service and configures Dependency Injection
        /// AddOltApiVersioning()
        /// AddOltDefault()
        /// AddOltApiSwagger()
        /// AddOltCors()
        /// AddOltJwt()
        /// AddControllers()
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="action">Invoked after initialized</param>
        /// <returns></returns>
        public static IServiceCollection AddOltAspNetCore<TSettings>(this IServiceCollection services, TSettings settings, Action<IMvcBuilder> action)
            where TSettings : OltAspNetAppSettings
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var assembliesToScan = new List<Assembly>
            {
                Assembly.GetEntryAssembly(),
                Assembly.GetExecutingAssembly()
            };

            assembliesToScan
                .GetAllReferencedAssemblies()
                .GetAllImplements<IOltAspNetCoreCorsPolicy>()
                .ToList()
                .ForEach(policy => services.AddOltCors(policy));

            services
                .AddOltApiVersioning()
                .AddOltApiSwagger(settings.Swagger);

            if (settings.JwtSecret.IsNotEmpty())
            {
                services.AddOltJwt(settings.JwtSecret);
            }

            services.AddOltDefault();

            services
                .AddSingleton<IOltHostService, OltHostAspNetCoreService>()
                .AddScoped<IOltIdentity, OltIdentityAspNetCore>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>())
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            action.Invoke(services.AddControllers());


            return services;
        }

        /// <summary>
        /// Adds CORS policy
        /// </summary>
        /// <param name="services"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltCors(this IServiceCollection services, IOltAspNetCoreCorsPolicy policy)
        {
            return policy.AddOltCors(services);
        }

        /// <summary>
        /// Adds Swagger Configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"><see cref="OltAspNetSwaggerAppSettings"/></param>
        /// <returns></returns>
        public static IServiceCollection AddOltApiSwagger(this IServiceCollection services, OltAspNetSwaggerAppSettings settings)
        {
            if (!settings.Enabled)
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
                                Title = $"{settings.Title}  {description.ApiVersion}",
                                Version = description.ApiVersion.ToString(),
                                Description = description.IsDeprecated ? $"{settings.Description} - DEPRECATED" : settings.Description,
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
                                Array.Empty<string>()
                            }
                        });

                        if (settings.XmlSettings.CommentsFilePath.IsNotEmpty())
                        {
                            options.IncludeXmlComments(settings.XmlSettings.CommentsFilePath, settings.XmlSettings.IncludeControllerXmlComments);
                        }
                        
                    });
        }

        /// <summary>
        /// Adds API version as query string and defaults to 1.0 if not present
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(
                    options =>
                    {
                        //The format of the version added to the route URL  
                        options.GroupNameFormat = "'v'VVV";
                        //Tells swagger to replace the version in the controller route  
                        options.SubstituteApiVersionInUrl = true;
                    })
                .AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                });

            return services;
        }


        /// <summary>
        /// Adds JWT Bearer Token Authentication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jwtSecret"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltJwt(this IServiceCollection services, string jwtSecret)
        {

            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new ArgumentNullException(nameof(jwtSecret));
            }

            var key = Encoding.ASCII.GetBytes(jwtSecret);
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = OltDefaults.JwtAuthenticationScheme;
                    x.DefaultChallengeScheme = OltDefaults.JwtAuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
#pragma warning disable S125
                    //x.Events = new JwtBearerEvents
                    //{
                    //    OnTokenValidated = context =>
                    //    {
                    //        //var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                    //        //var userId = int.Parse(context.Principal.Identity.Name);
                    //        //if (context.Principal.Identity is ClaimsIdentity identity)
                    //        //{
                    //        //    var userId = Convert.ToInt32(identity.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Upn)?.Value);
                    //        //}

                    //        //var user = userService.GetById(userId);
                    //        //if (user == null)
                    //        //{
                    //        //    // return unauthorized if user no longer exists
                    //        //    context.Fail("Unauthorized");
                    //        //}
                    //        return Task.CompletedTask;
                    //    }
                    //};
#pragma warning restore S125
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            return services;
        }
    }
}
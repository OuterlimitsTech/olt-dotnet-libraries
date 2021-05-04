using System;
using System.Collections.Generic;
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
        /// AddNewtonsoftJson();
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltAspNetCore( this IServiceCollection services, OltAspNetCoreOptions options)
        {

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            services
                .AddOltApiVersioning()
                .AddOltDefault(options)
                .AddSingleton<IOltHostService, OltHostAspNetCoreService>()
                .AddScoped<IOltIdentity, OltIdentityAspNetCore>()
                .AddScoped<IOltDbAuditUser>(x => x.GetRequiredService<IOltIdentity>())
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            if (options.DisableNewtonsoftJson)
            {
                services.AddControllers();
            }
            else
            {
                services
                    .AddControllers()
                    .AddNewtonsoftJson();
            }

            if (options.EnableSwagger)
            {
                services.AddOltApiSwagger();
            }

            if (options.EnableCores)
            {
                services.AddOltCors();
            }

            if (options.JwtSecret.IsNotEmpty())
            {
                services.AddOltJwt(options.JwtSecret);
            }

            return services;
        }


        /// <summary>
        /// Adds CORS using default name OltDefaultsAspNetCore.AspnetCorsPolicy
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltCors(this IServiceCollection services)
        {
            return services.AddCors(
                o => o.AddPolicy(OltDefaultsAspNetCore.AspnetCorsPolicy, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));
        }

        /// <summary>
        /// Adds Swagger Configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltApiSwagger(this IServiceCollection services)
        {
            var apiName = Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product ?? Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product;

            services
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
                                //Title = $"{Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product} {description.ApiVersion}",
                                Title = $"{apiName}  {description.ApiVersion}",
                                Version = description.ApiVersion.ToString(),
                                Description = description.IsDeprecated
                                    ? $"{Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "Api Methods"} - DEPRECATED"
                                    : Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "Api Methods",
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
                                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                                },
                                new string[] {}
                            }
                        });

                        //REMOVED - NOT USING 2020-02-02 Chris
                        // Tells swagger to pick up the output XML document file  
                        //options.IncludeXmlComments(Path.Combine(
                        //    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{this.GetType().Assembly.GetName().Name}.xml"
                        //));

                    });

            return services;
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
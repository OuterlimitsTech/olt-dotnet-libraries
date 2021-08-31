using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.AspNetCore;

namespace OLT.Logging.Serilog
{

    public static partial class OltExtensions
    {

        /// <summary>
        /// Registers OLT assets like middleware objects used for Serilog
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddOltSerilog(this IServiceCollection service)
        {
            return service.AddScoped<OltMiddlewareSession>();
        }

        private static IApplicationBuilder UseMiddleware<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsAspNetSerilog
        {

            if (options.DisableMiddlewareRegistration)
            {
                return app;
            }

            return app
                .UseMiddleware<OltMiddlewareSession>()
                .UseMiddleware<OltMiddlewarePayload>();
        }

        /// <summary>
        /// Registers middleware <seealso cref="SerilogApplicationBuilderExtensions"/>, <seealso cref="OltMiddlewareSession"/> and <seealso cref="OltMiddlewarePayload"/> using <seealso cref="IOltOptionsAspNetSerilog"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsAspNetSerilog"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSerilogRequestLogging<TOptions>(this IApplicationBuilder app, TOptions options)
                where TOptions : IOltOptionsAspNetSerilog
        {
            return app
                .UseSerilogRequestLogging()
                .UseMiddleware(options);
        }


        /// <summary>
        /// Registers middleware <seealso cref="SerilogApplicationBuilderExtensions"/>, <seealso cref="OltMiddlewareSession"/> and <seealso cref="OltMiddlewarePayload"/> using <seealso cref="IOltOptionsAspNetSerilog"/> 
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsAspNetSerilog"/></param>
        /// <param name="configureOptions"><seealso cref="RequestLoggingOptions"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSerilogRequestLogging<TOptions>(this IApplicationBuilder app, TOptions options, Action<RequestLoggingOptions> configureOptions)
                where TOptions : IOltOptionsAspNetSerilog
        {            

            return app
                .UseSerilogRequestLogging(configureOptions)
                .UseMiddleware(options);
        }
    }


    
}

using System;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.AspNetCore;

namespace OLT.Logging.Serilog
{

    public static partial class OltApplicationBuilderExtensions
    {

        private static IApplicationBuilder UseMiddleware<TOptions>(this IApplicationBuilder app, TOptions options)
            where TOptions : IOltOptionsSerilog
        {

            if (options.DisableMiddlewareRegistration)
            {
                return app;
            }

            return app
                .UseMiddleware<OltMiddlewareSessionLogging>()
                .UseMiddleware<OltMiddlewareDefault>();
        }

        /// <summary>
        /// Registers middlewares <seealso cref="SerilogApplicationBuilderExtensions"/>, <seealso cref="OltMiddlewareSessionLogging"/> and <seealso cref="OltMiddlewareHttpRequestBody"/> using <seealso cref="IOltOptionsSerilog"/> 
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsSerilog"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSerilogRequestLogging<TOptions>(this IApplicationBuilder app, TOptions options)
                where TOptions : IOltOptionsMessageTemplate
        {
            
            if (options.MessageTemplate.IsNotEmpty())
            {
                app.UseSerilogRequestLogging(options.MessageTemplate);
            }
            else
            {
                app.UseSerilogRequestLogging();
            }

            return app.UseMiddleware(options);
        }


        /// <summary>
        /// Registers middlewares <seealso cref="SerilogApplicationBuilderExtensions"/>, <seealso cref="OltMiddlewareSessionLogging"/> and <seealso cref="OltMiddlewareHttpRequestBody"/> using <seealso cref="IOltOptionsSerilog"/> 
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <param name="app"><seealso cref="IApplicationBuilder"/></param>
        /// <param name="options"><seealso cref="IOltOptionsSerilog"/></param>
        /// <param name="configureOptions"><seealso cref="RequestLoggingOptions"/></param>
        /// <returns><seealso cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSerilogRequestLogging<TOptions>(this IApplicationBuilder app, TOptions options, Action<RequestLoggingOptions> configureOptions)
                where TOptions : IOltOptionsSerilog
        {            

            return app
                .UseSerilogRequestLogging(configureOptions)
                .UseMiddleware(options);
        }
    }


    
}

using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NLog;


namespace OLT.Logging.NLog
{

    public static class OltAspNetCoreExceptionMiddlewareNLogExtension
    {
        /// <summary>
        /// Adds middleware for global error log logging.  
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="options"><seealso cref="OltOptionsNLog"/></param>
        public static void UseOltNLogExceptionLogging(this IApplicationBuilder app, Action<OltOptionsNLog> options) 
        {

            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        LogManager.GetCurrentClassLogger().Error(contextFeature.Error);

                        var config = new OltOptionsNLog();
                        options?.Invoke(config);
                        
                        var responseMessage = "An error has occurred.";

                        if (config.ShowExceptionDetails && contextFeature?.Error != null)
                        {
                            responseMessage = contextFeature.Error.Message;
                        }

                        var json = new OltErrorHttp { Message = responseMessage }.ToJson();
                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
    }
}

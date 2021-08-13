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
        /// <remarks>
        /// Will prevent the exception details from showing unless set using <param name="showExceptionDetails"></param>
        /// </remarks>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration"><seealso cref="LogConfig"/></param>
        public static void UseOltNLogExceptionLogging(this IApplicationBuilder app, Func<OltOptionsNLog, OltOptionsNLog> configuration) 
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
                        if (configuration != null)
                        {
                            config = configuration.Invoke(new OltOptionsNLog());
                        }
                        
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

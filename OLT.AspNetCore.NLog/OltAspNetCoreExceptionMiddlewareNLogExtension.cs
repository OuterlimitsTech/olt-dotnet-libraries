using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NLog;


namespace OLT.Core
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
        /// <param name="showExceptionDetails">Include Exception Details in the Response</param>
        /// <param name="defaultErrorMessage">Message returned when an error occurs</param>
        public static void UseOltNLogExceptionLogging(this IApplicationBuilder app, bool showExceptionDetails, string defaultErrorMessage = "Internal Server Error")
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

                        if (showExceptionDetails)
                        {
                            defaultErrorMessage = $"{defaultErrorMessage}{Environment.NewLine}Error:{Environment.NewLine}{contextFeature.Error}";
                        }

                        await context.Response.WriteAsync(new OltNLogError
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = defaultErrorMessage
                        }.ToString());
                    }
                });
            });
        }
    }
}

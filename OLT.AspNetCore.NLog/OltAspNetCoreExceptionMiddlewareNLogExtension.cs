using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NLog;
using OLT.Core;

namespace OLT.AspNetCore.NLog
{
    public static class OltAspNetCoreExceptionMiddlewareNLogExtension
    {
        public static void UseOltNLogRequestLogging(this IApplicationBuilder app, OltAspNetAppSettings settings)
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

                        var errorMsg = $"Internal Server Error.  Please contact support -> {settings.SupportEmail}";

                        if (settings.ShowExceptionDetails)
                        {
                            errorMsg = contextFeature.Error.ToString();
                        }

                        await context.Response.WriteAsync(new OltAspNetCoreError
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = errorMsg
                        }.ToString());
                    }
                });
            });
        }
    }
}

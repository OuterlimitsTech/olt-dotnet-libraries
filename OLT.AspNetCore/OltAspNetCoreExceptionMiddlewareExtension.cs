using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NLog;

namespace OLT.Core
{
    public static class OltAspNetCoreExceptionMiddlewareExtension
    {

        public static void UseOltExceptionHandler(this IApplicationBuilder app, bool showErrorDetails, string supportEmail = "support@outerlimitstech.com")
        {

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

                        var errorMsg = $"Internal Server Error.  Please contact support -> {supportEmail}";

                        if (showErrorDetails)
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
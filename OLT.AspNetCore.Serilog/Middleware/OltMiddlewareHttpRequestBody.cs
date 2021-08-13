using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OLT.Core;
using Serilog;

namespace OLT.Logging.Serilog
{
    public class OltMiddlewareDefault
    {
        private readonly RequestDelegate _next;

        public OltMiddlewareDefault(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestUri = $"{context.Request.Scheme}//{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
            var requestBodyText = await FormatRequest(context.Request);


            using (MemoryStream responseBodyStream = new MemoryStream())
            {
                var originalResponseBodyReference = context.Response.Body;
                context.Response.Body = responseBodyStream;
                try
                {
                    await _next(context);
                }
                catch (OltBadRequestException badRequestException)
                {
                    var msg = new OltErrorHttp { Message = badRequestException.Message };
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(msg.ToJson());
                }
                catch (OltValidationException validationException)
                {
                    var msg = new OltErrorHttp
                    {
                        Message = "An error has occurred.",
                        Errors = validationException.Results.Select(s => s.Message)
                    };
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(msg.ToJson());
                }
                catch (Exception exception)
                {
                    await context.Response.WriteAsync(FormatServerError(context, exception, requestBodyText));
                }

                var responseBodyText = await FormatResponse(context.Response);

                Log.ForContext("RequestHeaders", context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                    .ForContext("ResponseHeaders", context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                    .ForContext("RequestBody", requestBodyText)
                    .ForContext("ResponseBody", responseBodyText)
                    .ForContext("RequestUri", requestUri)
                    .Debug("OLT PAYLOAD LOG {RequestMethod} {RequestPath} {statusCode}", context.Request.Method, context.Request.Path, context.Response.StatusCode);

                await responseBodyStream.CopyToAsync(originalResponseBodyReference);
            }
        }

        private string FormatServerError(HttpContext context, Exception exception, string requestBodyText)
        {
            Guid errorId = Guid.NewGuid();
            Log.ForContext("RequestHeaders", context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestBody", requestBodyText)
                .ForContext("Exception", exception, destructureObjects: true)
                .Error(exception, exception.Message + ". {@errorId}", errorId);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var msg = new OltErrorHttp
            {
                Message = $"An error has occurred. {errorId}"
            };

#if DEBUG
            msg.Message = $"An error has occurred. {errorId}{Environment.NewLine}{exception}";
#endif
            return msg.ToJson();
        }


        private async Task<string> FormatRequest(HttpRequest request)
        {
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();
            var reader = new StreamReader(request.Body);
            string body = await reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return string.IsNullOrWhiteSpace(body) ? null : body;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return string.IsNullOrWhiteSpace(body) ? null : body;
        }
    }
}

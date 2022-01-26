using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OLT.Core;
using Serilog;
using Serilog.Events;

namespace OLT.Logging.Serilog
{
    public class OltMiddlewarePayload : IMiddleware
    {
        private readonly OltSerilogOptions _options;

        public OltMiddlewarePayload(IOptions<OltSerilogOptions> options)
        {
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Guid uid = Guid.NewGuid();
            var requestUri = $"{context.Request.Scheme}//{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
            var requestBodyText = await FormatRequestAsync(context.Request);
            var logLevel = LogEventLevel.Debug;

            await using MemoryStream responseBodyStream = new MemoryStream();
            var originalResponseBodyReference = context.Response.Body;
            context.Response.Body = responseBodyStream;

            try
            {
                await next(context);
            }
            catch (OltBadRequestException badRequestException)
            {
                var msg = new OltErrorHttpSerilog { ErrorUid = uid, Message = badRequestException.Message };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                logLevel = LogEventLevel.Warning;
                await context.Response.WriteAsync(msg.ToJson());
            }
            catch (OltValidationException validationException)
            {
                var msg = new OltErrorHttpSerilog
                {
                    ErrorUid = uid,
                    Message = "A validation error has occurred.",
                    Errors = validationException.Results.Select(s => s.Message)
                };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                logLevel = LogEventLevel.Warning;
                await context.Response.WriteAsync(msg.ToJson());
            }
            catch (OltRecordNotFoundException recordNotFoundException)
            {
                var msg = new OltErrorHttpSerilog { ErrorUid = uid, Message = recordNotFoundException.Message };
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                logLevel = LogEventLevel.Warning;
                await context.Response.WriteAsync(msg.ToJson());
            }
            catch (Exception exception)
            {
                logLevel = LogEventLevel.Error;
                var msg = FormatServerError(context, exception, uid);
                await context.Response.WriteAsync(msg.ToJson());
            }

            var responseBodyText = await FormatResponseAsync(context.Response);
            var logger = BuildLogger(context, uid, requestUri, requestBodyText, responseBodyText);
            logger.Write(logLevel, "{AppRequestUid}:APP PAYLOAD LOG {RequestMethod} {RequestPath} {statusCode}", uid, context.Request.Method, context.Request.Path, context.Response.StatusCode);

            await responseBodyStream.CopyToAsync(originalResponseBodyReference);
        }

        private static ILogger BuildLogger(HttpContext context, Guid uid, string requestUri, string requestBodyText, string responseBodyText)
        {
            return Log
                .ForContext("AppRequestUid", uid)
                .ForContext("RequestHeaders", context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("ResponseHeaders", context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestBody", requestBodyText)
                .ForContext("ResponseBody", responseBodyText)
                .ForContext("RequestUri", requestUri);
        }

        private OltErrorHttpSerilog FormatServerError(HttpContext context, Exception exception, Guid uid)
        {
            Log.ForContext("AppRequestUid", uid).Error(exception, "{AppRequestUid}:{Message}", exception.Message, uid);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var result = new OltErrorHttpSerilog { ErrorUid = uid, Message = _options.ErrorMessage };
            if (_options.ShowExceptionDetails)
            {
                result.Errors = exception.GetInnerExceptions().Select(s => s.Message).ToList();
            }
            return result;
        }


        private static async Task<string> FormatRequestAsync(HttpRequest request)
        {
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();
            var reader = new StreamReader(request.Body);
            string body = await reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return string.IsNullOrWhiteSpace(body) ? null : body;
        }

        private static async Task<string> FormatResponseAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return string.IsNullOrWhiteSpace(body) ? null : body;
        }
    }
}

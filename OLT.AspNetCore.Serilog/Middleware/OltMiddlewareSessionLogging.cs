using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OLT.Core;
using LogContext = Serilog.Context.LogContext;

namespace OLT.Logging.Serilog
{

    public class OltMiddlewareSessionLogging : IMiddleware
    {
        private readonly IOltIdentity _identity;

        public OltMiddlewareSessionLogging(IOltIdentity identity)
        {
            _identity = identity;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            using (LogContext.PushProperty("UserPrincipalName", _identity.UserPrincipalName))
            using (LogContext.PushProperty("Username", _identity.Username))
            using (LogContext.PushProperty("DbUsername", _identity.GetDbUsername()))
            {
                await next(context);
            }
            
        }
    }
}

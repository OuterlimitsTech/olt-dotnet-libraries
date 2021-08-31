using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace OLT.Core
{
    public static class OltHttpRequestExtensions
    {

        /// <summary>
        /// Retrieve the raw body as a string from the Request.Body stream
        /// </summary>
        /// <param name="request">Request instance to apply to</param>
        /// <param name="encoding">Optional - Encoding, defaults to UTF8</param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;

            using StreamReader reader = new StreamReader(request.Body, encoding);
            return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Retrieves the raw body as a byte array from the Request.Body stream
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request)
        {
            await using var ms = new MemoryStream(2048);
            await request.Body.CopyToAsync(ms);
            return ms.ToArray();
        }

        public static OltGenericParameter ToOltGenericParameter(this HttpRequest request)
        {
            var @params = request.RouteValues.ToOltGenericParameter().Values;
            request.Query.ToOltGenericParameter().Values.ToList().ForEach(x => @params.Add(x.Key, x.Value));
            return new OltGenericParameter(@params);
        }

        public static OltGenericParameter ToOltGenericParameter(this RouteValueDictionary value)
        {
            return new OltGenericParameter(value.ToDictionary(k => k.Key, v => v.Value?.ToString()));
        }

        public static OltGenericParameter ToOltGenericParameter(this IQueryCollection value)
        {
            return new OltGenericParameter(value.ToDictionary(k => k.Key, v => v.Value.ToString()));
        }

    }
}

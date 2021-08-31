using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;

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
            var dictionaries = new List<Dictionary<string, StringValues>>();

            try
            {
                dictionaries.Add(request.RouteValues?.ToDictionary(k => k.Key, v => new StringValues(v.Value?.ToString())));
            }
            catch
            {
                // Ignore
            }

            try
            {
                dictionaries.Add(request.Query?.ToDictionary(k => k.Key, v => v.Value));
            }
            catch
            {
                // Ignore
            }

            try
            {
                dictionaries.Add(request.Form?.ToDictionary(k => k.Key, v => v.Value));
            }
            catch
            {
                // Ignore
            }

            var merged = Merge(dictionaries).ToDictionary(x => x.Key, y => y.Value.ToString());

            return new OltGenericParameter(merged);
        }

        public static OltGenericParameter ToOltGenericParameter(this RouteValueDictionary value)
        {
            return new OltGenericParameter(value.ToDictionary(k => k.Key, v => v.Value?.ToString()));
        }

        public static OltGenericParameter ToOltGenericParameter(this IQueryCollection value)
        {
            return new OltGenericParameter(value.ToDictionary(k => k.Key, v => v.Value.ToString()));
        }

        public static OltGenericParameter ToOltGenericParameter(this IFormCollection value)
        {
            return new OltGenericParameter(value.ToDictionary(k => k.Key, v => v.Value.ToString()));
        }

        private static Dictionary<string, StringValues> Merge(IEnumerable<Dictionary<string, StringValues>> dictionaries)
        {
            Dictionary<string, StringValues> result = new Dictionary<string, StringValues>();

            foreach (Dictionary<string, StringValues> dict in dictionaries)
            {
                result
                    .Union(dict)
                    .GroupBy(g => g.Key)
                    .ToList()
                    .ForEach(item =>
                    {
                        if (!dict.ContainsKey(item.Key))
                        {
                            return;
                        }

                        var newValues = dict[item.Key];
                        if (result.ContainsKey(item.Key))
                        {
                            var currentValues = result[item.Key];
                            var concat = newValues.Concat(currentValues).ToArray();
                            result[item.Key] = new StringValues(concat);
                        }
                        else
                        {
                            result.Add(item.Key, new StringValues(newValues.ToArray()));
                        }

                    });
            }

            return result;
        }
    }
}

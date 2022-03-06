using System;
using System.Collections.Generic;
using System.Text.Json;

namespace OLT.Core
{
    public class OltErrorHttp : IOltErrorHttp
    {
        public Guid? ErrorUid { get; set; }

        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return System.Text.Json.JsonSerializer.Serialize(this, options);
        }

        public override string ToString()
        {
            return ToJson();
        }
    }
}

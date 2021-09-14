﻿using OLT.Core;
using System.Collections.Generic;
using System.Text.Json;

namespace OLT.Logging.NLog
{
    public class OltErrorHttp : IOltErrorHttp
    {        
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
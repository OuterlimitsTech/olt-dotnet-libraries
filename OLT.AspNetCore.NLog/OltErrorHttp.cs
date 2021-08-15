using OLT.Core;
using System.Collections.Generic;

namespace OLT.Logging.NLog
{
    public class OltErrorHttp : IOltErrorHttp
    {        
        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        public override string ToString()
        {
            return ToJson();
        }
    }
}
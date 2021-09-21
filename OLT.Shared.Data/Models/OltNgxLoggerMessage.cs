using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OLT.Core
{
    public class OltNgxLoggerMessage : IOltNgxLoggerMessage
    {
        public virtual string Message { get; set; }
        public virtual List<List<OltNgxLoggerDetail>> Additional { get; set; } = new List<List<OltNgxLoggerDetail>>();
        public virtual OltNgxLoggerLevel? Level { get; set; } = OltNgxLoggerLevel.Off;
        public virtual DateTimeOffset? Timestamp { get; set; }
        public virtual string FileName { get; set; }
        public virtual string LineNumber { get; set; }

        [JsonIgnore]
        public virtual string Username => Additional.FirstOrDefault()?.FirstOrDefault()?.User ?? "Unknown";

        [JsonIgnore]
        public virtual bool IsError => Level == OltNgxLoggerLevel.Error || Level == OltNgxLoggerLevel.Fatal;

        public virtual Exception ToException()
        {
            var detail = Additional.FirstOrDefault()?.FirstOrDefault();
            var ex = detail != null ? detail.ToException() : new Exception(Message);
            ex.Data.Add("Username", Username);
            ex.Data.Add("Level", Level?.ToString());
            ex.Data.Add("LineNumber", LineNumber);
            ex.Data.Add("FileName", FileName);
            ex.Data.Add("Timestamp", Timestamp?.ToISO8601());
            return ex;
        }
    }
}
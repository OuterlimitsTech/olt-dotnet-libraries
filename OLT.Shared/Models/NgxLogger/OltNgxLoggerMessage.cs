using System;
using System.Collections.Generic;
using System.Linq;


namespace OLT.Core
{
    
    public class OltNgxLoggerMessage : IOltNgxLoggerMessage
    {
        public virtual string Message { get; set; }
        public virtual List<List<OltNgxLoggerDetail>> Additional { get; set; } = new List<List<OltNgxLoggerDetail>>();
        public virtual OltNgxLoggerLevel? Level { get; set; } 
        public virtual DateTimeOffset? Timestamp { get; set; }
        public virtual string FileName { get; set; }
        public virtual string LineNumber { get; set; }

        public virtual string GetUsername()
        {
            return Additional.FirstOrDefault()?.FirstOrDefault()?.User ?? "Unknown";
        }

        public string FormatMessage()
        {
            return ToException().Message;
        }

        public virtual Exception ToException()
        {
            var detail = Additional.FirstOrDefault()?.FirstOrDefault();
            var ex = detail != null ? detail.ToException() : new Exception(Message);
            ex.Data.Add("Username", GetUsername());
            ex.Data.Add("Level", Level?.ToString());
            ex.Data.Add("LineNumber", LineNumber);
            ex.Data.Add("FileName", FileName);
            ex.Data.Add("Timestamp", Timestamp?.ToISO8601());
            return ex;
        }
    }
}
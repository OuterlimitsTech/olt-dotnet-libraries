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
        //public List<List<Dictionary<string,string>>> Additional { get; set; } = new List<List<Dictionary<string, string>>>();
        public virtual OltNgxLoggerLevel? Level { get; set; }
        public virtual DateTimeOffset? Timestamp { get; set; }
        public virtual string FileName { get; set; }
        public virtual string LineNumber { get; set; }

        [JsonIgnore]
        public virtual string Username => Additional.FirstOrDefault()?.FirstOrDefault()?.User;

        [JsonIgnore]
        public virtual bool IsError => Level == OltNgxLoggerLevel.Error || Level == OltNgxLoggerLevel.Fatal;

        public virtual Exception ToException()
        {
            var detail = Additional.FirstOrDefault()?.FirstOrDefault();
            var ex = detail != null ? detail.ToException() : new Exception(Message);
            //var ex = new Exception(Message);
            ex.Data.Add("Level", Level?.ToString());
            ex.Data.Add("LineNumber", LineNumber);
            ex.Data.Add("FileName", FileName);
            ex.Data.Add("Timestamp", Timestamp?.ToIso8601DateTimeString());
            return ex;
        }
    }
}
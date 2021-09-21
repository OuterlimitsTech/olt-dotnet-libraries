using System;
using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{
    public class OltNgxLoggerDetail
    {
        public virtual string Name { get; set; }
        public virtual string AppId { get; set; }
        public virtual string User { get; set; }
        public virtual long? Time { get; set; }
        public virtual string Id { get; set; }
        public virtual string Url { get; set; }
        public virtual object Status { get; set; }
        public virtual string Message { get; set; }
        public virtual List<OltNgxLoggerStack> Stack { get; set; }


        public Exception ToException()
        {
            var ex = new Exception(Message)
            {
                Source = Id
            };

            ex.Data.Add("Name", Name);
            ex.Data.Add("AppId", AppId);
            ex.Data.Add("User", User);
            if (Time.HasValue)
            {
                var dt = DateTimeOffset.FromUnixTimeMilliseconds(Time.Value);
                ex.Data.Add("Time", dt.ToISO8601());
            }
            else
            {
                ex.Data.Add("Time", null);
            }
            ex.Data.Add("Url", Url);
            ex.Data.Add("Status", Status);
            var stack = Stack?.Select(s => $"{s}{Environment.NewLine}{Environment.NewLine}").ToList();
            if (stack?.Count > 0)
            {
                ex.Data.Add("Stack", string.Join(Environment.NewLine, stack));
            }


            return ex;
        }
    }
}
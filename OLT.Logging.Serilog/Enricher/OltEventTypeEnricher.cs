using System;
using System.Text;
using Murmur;
using OLT.Core;
using Serilog.Core;
using Serilog.Events;

namespace OLT.Logging.Serilog.Enricher
{
    public class OltEventTypeEnricher : ILogEventEnricher
    {
        public const string PropertyName = OltDefaultsSerilog.Properties.EventType;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent is null)
                throw new ArgumentNullException(nameof(logEvent));

            if (propertyFactory is null)
                throw new ArgumentNullException(nameof(propertyFactory));

            Murmur32 murmur = MurmurHash.Create32();
            byte[] bytes = Encoding.UTF8.GetBytes(logEvent.MessageTemplate.Text);
            byte[] hash = murmur.ComputeHash(bytes);
            string hexadecimalHash = BitConverter.ToString(hash).Replace("-", "");
            LogEventProperty eventId = propertyFactory.CreateProperty(PropertyName, hexadecimalHash);
            logEvent.AddPropertyIfAbsent(eventId);
        }
    }
}
using System;

namespace OLT.Core
{
    public interface IOltEntityApplicationLog : IOltEntityId
    {
        string Application { get; }
        string CallSite { get; }
        DateTimeOffset Date { get; }
        string SourceName { get; }
        int? EventId { get; }
        string Username { get; }
        string Level { get; }
        string Logger { get; }
        string MachineName { get; }
        string Url { get; }
        string ServerAddress { get; }
        string RemoteAddress { get; }
        string Message { get; }
        string Exception { get; }
        string StackTrace { get; }
        string Payload { get; }
    }
}
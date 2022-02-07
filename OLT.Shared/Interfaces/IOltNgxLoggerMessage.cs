using System;
using System.Collections.Generic;

namespace OLT.Core
{

    public interface IOltNgxLoggerMessage
    {
        string Message { get; set; }
        List<List<OltNgxLoggerDetail>> Additional { get; set; }
        OltNgxLoggerLevel? Level { get; set; }
        DateTimeOffset? Timestamp { get; set; }
        string FileName { get; set; }
        string LineNumber { get; set; }
        string GetUsername();
        Exception ToException();
        string FormatMessage();
    }
}
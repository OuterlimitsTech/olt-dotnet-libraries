using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    [ExcludeFromCodeCoverage]
    [Obsolete]
    public static class EnumExtensions
    {             
        public static LogLevel ToLogLevel(this OltLogType logType)
        {
            switch (logType)
            {
                case OltLogType.Trace:
                    return LogLevel.Trace;
                case OltLogType.Debug:
                    return LogLevel.Debug;
                case OltLogType.Information:
                    return LogLevel.Information;
                case OltLogType.Warning:
                    return LogLevel.Warning;
                case OltLogType.Error:
                    return LogLevel.Error;
                case OltLogType.Critical:
                    return LogLevel.Critical;
                case OltLogType.None:
                    return LogLevel.None;
                default:
                    return LogLevel.Information;
            }
        }
    }
}
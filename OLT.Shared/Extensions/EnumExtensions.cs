using System;
using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    public static class EnumExtensions
    {
        public static LogLevel ToLogLevel(this OltNgxLoggerLevel ngxLoggerLevel)
        {
            switch (ngxLoggerLevel)
            {
                case OltNgxLoggerLevel.Trace:
                    return LogLevel.Trace;
                case OltNgxLoggerLevel.Debug:
                    return LogLevel.Debug;
                case OltNgxLoggerLevel.Info:
                    return LogLevel.Information;
                case OltNgxLoggerLevel.Log:
                    return LogLevel.Information;
                case OltNgxLoggerLevel.Warn:
                    return LogLevel.Warning;
                case OltNgxLoggerLevel.Error:
                    return LogLevel.Error;
                case OltNgxLoggerLevel.Fatal:
                    return LogLevel.Critical;
                case OltNgxLoggerLevel.Off:
                    return LogLevel.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ngxLoggerLevel), ngxLoggerLevel, null);
            }
        }


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
                    throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
            }
        }
    }
}
using System;
using NLog;
using NLog.Config;

namespace OLT.Core
{
    public class OltLogService : Logger, IOltLogService
    {
        private const string LoggerName = "NLogLogger";
        
        public static IOltLogService GetLoggingService()
        {
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("utc_date", typeof(UtcDateRenderer));
            var logger = (IOltLogService)LogManager.GetLogger("NLogLogger", typeof(OltLogService));
            return logger;
        }

        public virtual bool IsSqlTraceEnabled { get; set; } = false;

        public virtual void SqlTrace(string message)
        {
            if (IsSqlTraceEnabled)
            {
                Write(OltLogType.Trace, message);
            }
        }

        public virtual void Write(OltLogType logType, string message)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var logEventInfo = GetLogEvent(LoggerName, LogLevel.FromOrdinal((int)logType), null, null);
            logEventInfo.Message = message;
            logger.Log(logEventInfo);
        }

        //public virtual void Write(OltLogType logType, string message, string format, params object[] args)
        //{
        //    var logger = LogManager.GetCurrentClassLogger();
        //    var logEventInfo = GetLogEvent(LoggerName, LogLevel.FromOrdinal((int)logType), null, format, args);
        //    logEventInfo.Message = message;
        //    logger.Log(logEventInfo);
        //}

        //public virtual void Write(Exception exception, string message, params object[] args)
        //{
        //    var logger = LogManager.GetCurrentClassLogger();
        //    logger.Log(LogLevel.Error, exception, CultureInfo.CurrentCulture, message, args);
        //}

        public virtual void Write(Exception exception, string message)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var logEventInfo = GetLogEvent(LoggerName, LogLevel.Error, exception, null);
            logEventInfo.Message = message;
            logger.Log(logEventInfo);
        }

        public virtual void Write(Exception exception)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var logEventInfo = GetLogEvent(LoggerName, LogLevel.Error, exception, null);
            logger.Log(logEventInfo);
        }

        public virtual void Write(IOltNgxLoggerMessage loggerMessage, string userName)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var logEventInfo = GetLogEvent(LoggerName, LogLevel.Error, loggerMessage.ToException(), null);
            logEventInfo.Message = $"Ngx User: {userName}";
            logger.Log(logEventInfo);
        }


        protected virtual LogEventInfo GetLogEvent(string loggerName, LogLevel level, Exception exception, string format, params object[] args)
        {
            var assemblyProp = string.Empty;
            var classProp = string.Empty;
            var methodProp = string.Empty;
            var messageProp = string.Empty;
            var innerMessageProp = string.Empty;


            var logEvent = !string.IsNullOrWhiteSpace(format) && args != null && args.Length > 0 ? new LogEventInfo(level, loggerName, string.Format(format, args)) :
                new LogEventInfo(level, loggerName, null);

            if (exception != null)
            {
                assemblyProp = exception.Source;
                classProp = exception.TargetSite?.DeclaringType?.FullName;
                methodProp = exception.TargetSite?.Name;
                messageProp = exception.Message;

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }

            logEvent.Properties["error-source"] = assemblyProp;
            logEvent.Properties["error-class"] = classProp;
            logEvent.Properties["error-method"] = methodProp;
            logEvent.Properties["error-message"] = messageProp;
            logEvent.Properties["inner-error-message"] = innerMessageProp;

            return logEvent;
        }

        /// <summary>
        /// The disposed
        /// </summary>
        protected virtual bool Disposed { get; set; } = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
    }
}

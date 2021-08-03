using System;
using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    public class OltLogService : IOltLogService
    {
        private readonly ILogger<OltLogService> _logger;

        public OltLogService(ILogger<OltLogService> logger)
        {
            _logger = logger;
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
            _logger.Log(logType.ToLogLevel(), message);
        }

        public virtual void Write(OltLogType logType, string message, params object[] args)
        {
            _logger.Log(logType.ToLogLevel(), message, args);
        }

        public virtual void Write(Exception exception)
        {            
            Write(exception, null, null);
        }


        public virtual void Write(Exception exception, string message)
        {
            Write(exception, message, null);
        }

        public virtual void Write(Exception exception, string message, params object[] args)
        {
            _logger.LogError(exception, message, args);
        }

        public virtual void Write(IOltNgxLoggerMessage loggerMessage, string userName)
        {
            //var logger = LogManager.GetCurrentClassLogger();
            //var logEventInfo = GetLogEvent(LoggerName, LogLevel.Error, loggerMessage.ToException(), null);
            //logEventInfo.Message = $"Ngx User: {userName}";
            //logger.Log(logEventInfo);
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

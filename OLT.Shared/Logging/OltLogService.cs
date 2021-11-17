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
            var message = $"Token User: {userName}{Environment.NewLine}{loggerMessage.Message}";
            if (loggerMessage.IsError)
            {
                _logger.LogError(loggerMessage.ToException(), message);
                return;
            }
            var logType = loggerMessage.Level.GetValueOrDefault(OltNgxLoggerLevel.Info).ToLogLevel();
            _logger.Log(logType, $"{message}{Environment.NewLine}{Environment.NewLine}{loggerMessage.ToException().Message}");
        }


        /// <summary>
        /// The disposed
        /// </summary>
        protected virtual bool Disposed { get; set; } = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            Disposed = true;
        }
    }
}
 
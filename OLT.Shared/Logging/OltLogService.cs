using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    [Obsolete]
    [ExcludeFromCodeCoverage]
    public class OltLogService : OltDisposable, IOltLogService
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

        //public virtual void Write(IOltNgxLoggerMessage loggerMessage, string userName)
        //{
        //    var message = $"Token User: {userName}{Environment.NewLine}{loggerMessage.Message}";            
        //    if (loggerMessage.Level == OltNgxLoggerLevel.Error || loggerMessage.Level == OltNgxLoggerLevel.Fatal)
        //    {
        //        _logger.LogError(loggerMessage.ToException(), message);
        //        return;
        //    }
        //    var logType = loggerMessage.Level.GetValueOrDefault(OltNgxLoggerLevel.Info).ToLogLevel();
        //    _logger.Log(logType, $"{message}{Environment.NewLine}{Environment.NewLine}{loggerMessage.FormatMessage()}");
        //}
      
    }
}
 
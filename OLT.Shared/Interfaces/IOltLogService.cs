using System;

namespace OLT.Core
{
    public interface IOltLogService : IOltInjectableSingleton
    {
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsTraceEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsSqlTraceEnabled { get; }

        void SqlTrace(string message);

        /// <summary>
        /// Writes the specified log type.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        /// <param name="message">The message.</param>
        void Write(OltLogType logType, string message);

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void Write(Exception exception);

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        void Write(Exception exception, string message);

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="loggerMessage">Angular NGX Log Body</param>
        /// <param name="userName">The current logged in user (if applicable).</param>
        void Write(IOltNgxLoggerMessage loggerMessage, string userName);




    }
}
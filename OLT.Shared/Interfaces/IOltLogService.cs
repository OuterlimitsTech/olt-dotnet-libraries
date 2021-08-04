using System;

namespace OLT.Core
{
    public interface IOltLogService : IOltInjectableSingleton
    {
        //bool IsDebugEnabled { get; }
        //bool IsErrorEnabled { get; }
        //bool IsFatalEnabled { get; }
        //bool IsInfoEnabled { get; }
        //bool IsTraceEnabled { get; }
        //bool IsWarnEnabled { get; }
        //bool IsSqlTraceEnabled { get; }

        void SqlTrace(string message);

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="oltLogType">Entry will be written on this level.</param>
        /// <param name="message">Format string of the log message.</param>
        void Write(OltLogType oltLogType, string message);

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="oltLogType">Entry will be written on this level.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void Write(OltLogType oltLogType, string message, params object[] args);

        /// <summary>Formats and writes an error log message.</summary>
        /// <param name="exception">The exception to log.</param>
        /// <example>logger.LogError(exception)</example>
        void Write(Exception exception);

        /// <summary>Formats and writes an error log message.</summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The log message</param>
        /// <example>logger.LogError(exception, "Error while processing request from {Address}")</example>
        void Write(Exception exception, string message);

        /// <summary>Formats and writes an error log message.</summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError(exception, "Error while processing request from {Address}", address)</example>
        void Write(Exception exception, string message, params object[] args);

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="loggerMessage">Angular NGX Log Body</param>
        /// <param name="userName">The current logged in user (if applicable).</param>
        void Write(IOltNgxLoggerMessage loggerMessage, string userName);




    }
}
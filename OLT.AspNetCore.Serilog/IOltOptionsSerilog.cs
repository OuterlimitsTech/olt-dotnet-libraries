namespace OLT.Logging.Serilog
{
    public interface IOltOptionsSerilog
    {
        /// <summary>
        /// Disable auto registration of <seealso cref="OltMiddlewareSessionLogging"/> and <seealso cref="OltMiddlewareHttpRequestBody"/>
        /// </summary>
        bool DisableMiddlewareRegistration { get; }

    }

    public interface IOltOptionsMessageTemplate : IOltOptionsSerilog
    {
        /// <summary>
        /// OPTIONAL!!! Override configured message template
        /// </summary>
        string MessageTemplate { get; }

    }

}

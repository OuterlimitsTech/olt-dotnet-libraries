namespace OLT.Logging.Serilog
{
    public interface IOltOptionsAspNetSerilog
    {
        /// <summary>
        /// Disable auto registration of <seealso cref="OltMiddlewareSessionLogging"/> and <seealso cref="OltMiddlewareDefault"/>
        /// </summary>
        bool DisableMiddlewareRegistration { get; }

        /// <summary>
        /// OPTIONAL!!! Override configured message template
        /// </summary>
        string MessageTemplate { get; }
    }


}

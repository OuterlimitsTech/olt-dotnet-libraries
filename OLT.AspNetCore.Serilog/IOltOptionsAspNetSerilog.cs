namespace OLT.Logging.Serilog
{
    public interface IOltOptionsAspNetSerilog
    {
        /// <summary>
        /// Disable auto registration of <seealso cref="OltMiddlewareSession"/> and <seealso cref="OltMiddlewarePayload"/>
        /// </summary>
        bool DisableMiddlewareRegistration { get; }

        /// <summary>
        /// Message template to use.
        /// </summary>
        /// <remarks>
        /// Default will use <seealso cref="OltDefaultsSerilog.Templates.DefaultOutput"/>
        /// </remarks>
        string MessageTemplate { get; }
    }


}

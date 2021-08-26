namespace OLT.Logging.Serilog
{
    public class OltOptionsAspNetSerilog : IOltOptionsAspNetSerilog
    {
        /// <summary>
        /// Message template to use.
        /// </summary>
        /// <remarks>
        /// Default will use <seealso cref="OltDefaultsSerilog.Templates.DefaultOutput"/>
        /// </remarks>
        public string MessageTemplate { get; set; } = OltDefaultsSerilog.Templates.DefaultOutput;

        /// <summary>
        /// Disable auto registration of <seealso cref="OltMiddlewareSession"/> and <seealso cref="OltMiddlewarePayload"/>
        /// </summary>
        public bool DisableMiddlewareRegistration { get; set; }
    }


    
}

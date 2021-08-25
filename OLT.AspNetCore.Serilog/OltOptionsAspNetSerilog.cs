namespace OLT.Logging.Serilog
{
    public class OltOptionsAspNetSerilog : IOltOptionsAspNetSerilog
    {
        /// <summary>
        /// OPTIONAL!!! Override configured message template
        /// </summary>
        public string MessageTemplate { get; set; }

        /// <summary>
        /// Disable auto registration of <seealso cref="OltMiddlewareSessionLogging"/> and <seealso cref="OltMiddlewareHttpRequestBody"/>
        /// </summary>
        public bool DisableMiddlewareRegistration { get; set; }
    }


    
}

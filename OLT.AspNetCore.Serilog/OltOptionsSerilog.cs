namespace OLT.Logging.Serilog
{
    public class OltOptionsSerilog : IOltOptionsMessageTemplate
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

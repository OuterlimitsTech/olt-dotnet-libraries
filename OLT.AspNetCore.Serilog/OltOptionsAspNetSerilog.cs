namespace OLT.Logging.Serilog
{
    public class OltOptionsAspNetSerilog : IOltOptionsAspNetSerilog
    {
        /// <summary>
        /// Disable auto registration of <seealso cref="OltMiddlewareSession"/> and <seealso cref="OltMiddlewarePayload"/>
        /// </summary>
        public bool DisableMiddlewareRegistration { get; set; }
    }


    
}

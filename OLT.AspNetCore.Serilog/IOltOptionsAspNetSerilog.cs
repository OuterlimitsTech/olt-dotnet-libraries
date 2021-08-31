namespace OLT.Logging.Serilog
{
    public interface IOltOptionsAspNetSerilog
    {
        /// <summary>
        /// Disable auto registration of <seealso cref="OltMiddlewareSession"/> and <seealso cref="OltMiddlewarePayload"/>
        /// </summary>
        bool DisableMiddlewareRegistration { get; }

    }


}

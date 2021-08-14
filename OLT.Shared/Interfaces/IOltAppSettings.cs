namespace OLT.Core
{
    public interface IOltAppSettings
    {
        /// <summary>
        /// Support email shown on exception responses. 
        /// </summary>
        /// <remarks>
        /// Default: support@outerlimitstech.com
        /// </remarks>        
        string SupportEmail { get; }
    }
}
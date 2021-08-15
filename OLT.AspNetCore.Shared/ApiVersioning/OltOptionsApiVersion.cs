namespace OLT.Core
{
    public class OltOptionsApiVersion : IOltOptionsApiVersion
    {

        /// <summary>
        /// Enable Token Authentication
        /// </summary>
        /// <remarks>
        /// Default is true
        /// </remarks>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Indicating whether a default version is assumed when a client does not provide a service API version.
        /// </summary>
        /// <remarks>
        /// Default true
        /// </remarks>
        public bool AssumeDefaultVersionWhenUnspecified { get; set; } = true;

        /// <summary>
        /// Reads the API Version from the query string
        /// </summary>
        /// <remarks>
        /// Default api-version
        /// </remarks>
        public string ApiQueryParameterName { get; set; } = "api-version";
    }
}
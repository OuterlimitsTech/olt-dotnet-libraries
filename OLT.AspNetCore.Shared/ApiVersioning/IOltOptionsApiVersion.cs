namespace OLT.Core
{
    public interface IOltOptionsApiVersion
    {
        /// <summary>
        /// Enable Token Authentication
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Indicating whether a default version is assumed when a client does not provide a service API version.
        /// </summary>
        bool AssumeDefaultVersionWhenUnspecified { get; }

        /// <summary>
        /// Reads the API Version from the query string
        /// </summary>
        string ApiQueryParameterName { get; }
    }
}
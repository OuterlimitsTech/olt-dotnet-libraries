

namespace OLT.Core
{
    public interface IOltOptionsAspNetHosting
    {
        /// <summary>
        /// CORS Policy to apply
        /// </summary>
        /// <remarks>
        /// Default Value is Olt_CorsPolicy_Disabled
        /// </remarks>
        string CorsPolicyName { get; }

        /// <summary>
        /// Disables middleware for redirecting HTTP Requests to HTTPS.
        /// </summary>
        bool DisableHttpsRedirect { get; }

        
        /// <summary>
        /// Adds a middleware that extracts the specified path base from request path and postpend it to the request path base.
        /// </summary>
        string PathBase { get; }

        /// <summary>
        /// Enables UseDeveloperExceptionPage();
        /// </summary>
        /// <remarks>
        /// This should only be enabled in the Development environment. 
        /// </remarks>
        /// <remarks>Default: false</remarks>
        bool ShowExceptionDetails { get; }

        /// <summary>
        /// Adds middleware for using HSTS, which adds the Strict-Transport-Security header.
        /// </summary>
        bool UseHsts { get; }
    }
}
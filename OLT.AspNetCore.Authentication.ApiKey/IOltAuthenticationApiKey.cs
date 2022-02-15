using AspNetCore.Authentication.ApiKey;

namespace OLT.AspNetCore.Authentication
{
    public interface IOltAuthenticationApiKey : IOltAuthenticationSchemeBuilder<ApiKeyOptions>
    {
        /// <summary>
        /// <seealso cref="ApiKeyOptions.Realm"/>
        /// Gets or sets the realm property. It is used with WWW-Authenticate response header when challenging un-authenticated requests.
        /// Required to be set if SuppressWWWAuthenticateHeader is not set to true.
        /// <see href="https://tools.ietf.org/html/rfc7235#section-2.2"/>
        /// </summary>
        string Realm { get; }

        /// <summary>
        /// It is the name of the header or query parameter of the API Key.
        /// </summary>
        /// <remarks>
        /// Defaulted to "X-API-KEY"
        /// </remarks>
        string KeyName { get; }
    }
}
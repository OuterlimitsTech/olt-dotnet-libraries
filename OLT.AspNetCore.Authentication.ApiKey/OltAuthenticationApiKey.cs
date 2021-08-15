using System;
using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using OLT.Core;

namespace OLT.AspNetCore.Authentication
{
    public class OltAuthenticationApiKey<TProvider> : OltAuthenticationSchemeBuilder<ApiKeyOptions>, IOltAuthenticationApiKey
        where TProvider : class, IApiKeyProvider
    {

        public OltAuthenticationApiKey(string realm)
        {
            Realm = realm;
        }

        /// <summary>
        /// Default is true
        /// </summary>
        public virtual bool Enabled { get; set; } = true;

        /// <summary>
        /// Default <seealso cref="ApiKeyDefaults.AuthenticationScheme"/>
        /// </summary>
        public override string Scheme => ApiKeyDefaults.AuthenticationScheme;

        /// <summary>
        /// <seealso cref="AspNetCore.Authentication.ApiKey.ApiKeyOptions.Realm"/>
        /// Gets or sets the realm property. It is used with WWW-Authenticate response header when challenging un-authenticated requests.
        /// Required to be set if SuppressWWWAuthenticateHeader is not set to true.
        /// <see href="https://tools.ietf.org/html/rfc7235#section-2.2"/>
        /// </summary>
        public virtual string Realm { get; set; }

        /// <summary>
        /// It is the name of the header or query parameter of the API Key.
        /// </summary>
        /// <remarks>
        /// Defaulted to "X-API-KEY"
        /// </remarks>
        public virtual string KeyName { get; set; } = "X-API-KEY";


        public override AuthenticationBuilder AddScheme(AuthenticationBuilder builder, Action<ApiKeyOptions> configureOptions)
        {
            if (Enabled)
            {
                builder
                    .AddApiKeyInHeaderOrQueryParams<TProvider>(opt =>
                    {
                        opt.Realm = Realm;
                        opt.KeyName = KeyName;
                        configureOptions?.Invoke(opt);
                    });
            }

            return builder;
        }

       

    }


}

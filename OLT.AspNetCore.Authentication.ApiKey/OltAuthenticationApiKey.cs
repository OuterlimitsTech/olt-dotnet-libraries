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

        public enum OltApiKeyLocation
        {
            HeaderOrQueryParams,
            HeaderOnly,
            QueryParamsOnly
        }

        public OltAuthenticationApiKey(string realm)
        {
            Realm = realm;
        }

        public OltAuthenticationApiKey(string realm, OltApiKeyLocation apiKeyLocation)
        {
            Realm = realm;
            ApiKeyLocation = apiKeyLocation;
        }


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


        /// <summary>
        /// API Key location <seealso cref="OltApiKeyLocation"/>
        /// </summary>
        /// <remarks>
        /// Defaults to <seealso cref="OltApiKeyLocation.HeaderOrQueryParams"/>
        /// </remarks>
        public virtual OltApiKeyLocation ApiKeyLocation { get; set; } = OltApiKeyLocation.HeaderOrQueryParams;



        /// <summary>
        /// Adds API Key Scheme middlware authentication to ASP.Net Core
        /// </summary>
        /// <param name="builder"><seealso cref="AuthenticationBuilder"/></param>
        /// <param name="configureOptions"><seealso cref="ApiKeyOptions"/></param>
        /// <returns><seealso cref="AuthenticationBuilder"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public override AuthenticationBuilder AddScheme(AuthenticationBuilder builder, Action<ApiKeyOptions> configureOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            switch (ApiKeyLocation)
            {
                case OltApiKeyLocation.HeaderOnly:
                    return builder
                        .AddApiKeyInHeader<TProvider>(opt =>
                        {
                            opt.Realm = Realm;
                            opt.KeyName = KeyName;
                            configureOptions?.Invoke(opt);
                        });

                case OltApiKeyLocation.QueryParamsOnly:
                    return builder
                    .AddApiKeyInQueryParams<TProvider>(opt =>
                    {
                        opt.Realm = Realm;
                        opt.KeyName = KeyName;
                        configureOptions?.Invoke(opt);
                    });

                case OltApiKeyLocation.HeaderOrQueryParams:
                    return builder
                        .AddApiKeyInHeaderOrQueryParams<TProvider>(opt =>
                        {
                            opt.Realm = Realm;
                            opt.KeyName = KeyName;
                            configureOptions?.Invoke(opt);
                        });
            }

            throw new InvalidOperationException($"{nameof(ApiKeyLocation)} - invalid");
            
        }

       

    }


}

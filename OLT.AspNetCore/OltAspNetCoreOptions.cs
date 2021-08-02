using System.Collections.Generic;

namespace OLT.Core
{

    public class OltAspNetCoreOptions<TSettings> : OltInjectionOptions
        where TSettings : OltAspNetAppSettings
    {

        public OltAspNetCoreOptions(TSettings settings, string jwtSecret, bool? disableNewtonsoftJson = false, int? cacheExpirationMinutes = 30)
        {
            Settings = settings;
            JwtSecret = jwtSecret;
            DisableNewtonsoftJson = disableNewtonsoftJson.GetValueOrDefault(false);
            CacheExpirationMinutes = cacheExpirationMinutes.GetValueOrDefault(30);
        }

        public TSettings Settings { get; }
        public bool DisableNewtonsoftJson { get; set; } = false;
        public string JwtSecret { get; set; }

        /// <summary>
        /// List of Cores and custom ones can be added
        /// </summary>
        public List<IOltAspNetCoreCorsPolicy> CorsPolicies { get; } = new List<IOltAspNetCoreCorsPolicy>
        {
            new OltAspNetCoreCorsPolicyDisabled(),
            new OltAspNetCoreCorsPolicyWildcard()
        };
    }
}
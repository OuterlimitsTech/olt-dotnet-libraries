using System.Collections.Generic;

namespace OLT.Core
{

    public class OltAspNetCoreOptions : OltInjectionOptions
    {
        public bool EnableSwagger { get; set; } = false;
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
using System;

namespace OLT.Core
{
    public class OltAspNetAppSettings : IOltAppSettings
    {

        /// <summary>
        /// Hosting Settings
        /// </summary>
        public OltAspNetHostingAppSettings Hosting { get; set; } = new OltAspNetHostingAppSettings();

        /// <summary>
        /// Swagger Options
        /// </summary>
        public virtual OltAspNetSwaggerAppSettings Swagger { get; set; } = new OltAspNetSwaggerAppSettings();

        /// <summary>
        /// Support email shown on exception responses. 
        /// </summary>
        /// <remarks>
        /// Default: support@outerlimitstech.com
        /// </remarks>
        public virtual string SupportEmail { get; set; } = "support@outerlimitstech.com";

        /// <summary>
        /// JWT Secret for creating JWT Tokens
        /// </summary>
        /// <remarks>
        /// If empty or null, Jwt Token Authentication will not be enabled.
        /// </remarks>
        public string JwtSecret { get; set; }
    }
}
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
        /// CORS Policy to apply
        /// </summary>
        /// <remarks>
        /// Default Value is Olt_CorsPolicy_Disabled
        /// </remarks>
        public virtual string CorsPolicyName { get; set; } = OltAspNetDefaults.CorsPolicyName;

        /// <summary>
        /// Swagger Options
        /// </summary>
        public virtual OltAspNetSwaggerAppSettings Swagger { get; set; } = new OltAspNetSwaggerAppSettings();

        /// <summary>
        /// Enables UseDeveloperExceptionPage();
        /// </summary>
        /// <remarks>
        /// This should only be enabled in the Development environment. 
        /// </remarks>
        /// <remarks>Default: false</remarks>
        public virtual bool ShowExceptionDetails { get; set; }

        /// <summary>
        /// Support email shown on exception responses. 
        /// </summary>
        /// <remarks>
        /// Default: support@outerlimitstech.com
        /// </remarks>
        public virtual string SupportEmail { get; set; } = "support@outerlimitstech.com";

    }
}
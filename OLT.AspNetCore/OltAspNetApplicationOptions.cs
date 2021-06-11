using System.Reflection;

namespace OLT.Core
{
    public class OltAspNetApplicationCorsOption
    {

        public OltAspNetApplicationCorsOption(IOltAspNetCoreCorsPolicy policy)
        {
            Policy = policy;
        }

        /// <summary>
        /// Default of true and applies default OltAspNetCoreCorsPolicyDisabled Policy
        /// </summary>
        public bool UseCors { get; set; } = true;

        /// <summary>
        /// The Cross-Origin Resource Sharing (CORS) policy to apply
        /// </summary>
        /// <value>OltAspNetCoreCorsPolicyDisabled</value>
        public IOltAspNetCoreCorsPolicy Policy { get; } 
    }

    public class OltAspNetApplicationOptions
    {
        /// <summary>
        /// AppName used for Swagger.  This will default to the AssemblyProductAttribute name of Assembly
        /// </summary>
        public string SwaggerAppName { get; set; } =
            Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ??
            Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ?? 
            "OLT App Unknown";

        public bool EnableDeveloperExceptionPage { get; set; } = false;
        public bool EnableSwagger { get; set; } = false;

        /// <summary>
        /// The Cross-Origin Resource Sharing (CORS) policy to apply
        /// UseCors 
        /// </summary>
        /// <value>OltAspNetCoreCorsPolicyDisabled</value>
        public OltAspNetApplicationCorsOption Cors { get; set; } = new OltAspNetApplicationCorsOption(new OltAspNetCoreCorsPolicyDisabled());

        public bool DisableHttpsRedirect { get; set; } = false;
        public bool DisableUseAuthentication { get; set; } = false;
        public bool ShowErrorDetails { get; set; } = false;

        /// <summary>
        /// Used to "EnableBuffering" required for NLog ${aspnet-request-posted-body} logging
        /// Default: true
        /// </summary>
        public bool EnableBuffering { get; set; } = true;

        /// <summary>
        /// Support email shown on exception responses.
        /// Default: support@outerlimitstech.com
        /// </summary>
        public string SupportEmail { get; set; } = "support@outerlimitstech.com";
    }
}
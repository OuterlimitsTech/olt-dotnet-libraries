using System.Reflection;

namespace OLT.Core
{
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
        public bool EnableCors { get; set; } = false;
        public bool DisableHttpsRedirect { get; set; } = false;
        public bool DisableUseAuthentication { get; set; } = false;
        public bool ShowErrorDetails { get; set; } = false;

        /// <summary>
        /// Support email shown on exception responses.
        /// Default: support@outerlimitstech.com
        /// </summary>
        public string SupportEmail { get; set; } = "support@outerlimitstech.com";
    }
}
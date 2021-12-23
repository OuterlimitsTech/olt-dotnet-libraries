using System.Reflection;

namespace OLT.Core
{
    public class OltOptionsAspNetSwagger : IOltOptionsAspNetSwaggerUI
    {
        /// <summary>
        /// Enable Swagger Document
        /// </summary>
        /// <remarks>
        /// In most cases, this should only be enabled in the Development environment. 
        /// </remarks>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// Swagger XML Settings
        /// </summary>
        public IOltOptionsAspNetSwaggerXml XmlSettings { get; set; } = new OltOptionsAspNetSwaggerXml();

        /// <summary>
        /// Title used for Swagger.  This will default to the AssemblyProductAttribute name of Assembly
        /// </summary>
        /// <remarks>Default is Web Api</remarks>
        public virtual string Title { get; set; } =
            Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ??
            Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ??
            "Web Api";

        /// <summary>
        /// Title used for Swagger.  This will default to the AssemblyDescriptionAttribute name of Assembly 
        /// </summary>
        /// <remarks>Default is Api Methods</remarks>
        public virtual string Description { get; set; } =
            Assembly.GetEntryAssembly()?.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description ??
            Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyDescriptionAttribute>()?.Description ??
            "Api Methods";
    }
}
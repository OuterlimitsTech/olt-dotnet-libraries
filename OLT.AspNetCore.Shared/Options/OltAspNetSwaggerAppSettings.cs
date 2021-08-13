using System.Reflection;

namespace OLT.Core
{

    public class OltOptionsAspNetSwaggerXml : IOltOptionsAspNetSwaggerXml
    {
        /// <summary>
        /// OPTIONAL: File that contains XML Comments to display in Swagger. 
        /// </summary>
        /// <remarks>
        /// Inject human-friendly descriptions for Operations, Parameters and Schemas based on XML Comment files
        /// </remarks>
        /// <remarks>
        /// If not provided, the IncludeXmlComments option will not be set />
        /// </remarks>
        public virtual string CommentsFilePath { get; set; }


        /// <summary>
        /// Flag to indicate if controller XML comments (i.e. summary) should be used to assign Tag descriptions.
        /// Don't set this flag if you're customizing the default tag for operations via TagActionsBy.
        /// </summary>
        public virtual bool IncludeControllerXmlComments { get; set; }

    }

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
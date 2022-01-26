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
}
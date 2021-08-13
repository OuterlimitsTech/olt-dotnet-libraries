namespace OLT.Core
{
    public abstract class OltAspNetAppSettings : OltAppSettings, IOltOptionsAspNet
    {
        /// <summary>
        /// Hosting Settings
        /// </summary>
        public IOltOptionsAspNetHosting Hosting { get; set; } = new OltAspNetHostingOptions();

        /// <summary>
        /// Swagger Options
        /// </summary>
        public virtual IOltOptionsAspNetSwaggerUI Swagger { get; set; } = new OltOptionsAspNetSwagger();


        
    }
}
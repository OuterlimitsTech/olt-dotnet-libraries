namespace OLT.Core
{
    public class OltAspNetAppSettings : OltAppSettings, IOltOptionsAspNet
    {
        /// <summary>
        /// Hosting Settings
        /// </summary>
        public virtual IOltOptionsAspNetHosting Hosting { get; set; } = new OltAspNetHostingOptions();
        
    }
}
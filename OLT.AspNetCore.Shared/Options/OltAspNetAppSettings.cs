namespace OLT.Core
{
    public abstract class OltAspNetAppSettings : OltAppSettings, IOltOptionsAspNet //, IOltOptionsAspNet<TAuthentication>
        //where TAuthentication : class, IOltOptionsAuthentication, new()
    {
        /// <summary>
        /// Hosting Settings
        /// </summary>
        public IOltOptionsAspNetHosting Hosting { get; set; } = new OltAspNetHostingOptions();

        //public TAuthOptions GetAuthentication<TAuthOptions>() 
        //    where TAuthOptions : class, IOltOptionsAuthentication, new()
        //{
        //    return Authentication as TAuthOptions;
        //}

        /// <summary>
        /// Swagger Options
        /// </summary>
        public virtual IOltOptionsAspNetSwaggerUI Swagger { get; set; } = new OltOptionsAspNetSwagger();


        /////// <summary>
        /////// ApiVersioning to apply
        /////// </summary>
        /////// <remarks>
        /////// Default Enabled with url?api-version=1.0
        /////// </remarks>
        ////public IOltOptionsApiVersion ApiVersion { get; set; } = new OltOptionsApiVersion();


        //public TAuthentication Authentication { get; set; } = new TAuthentication();

        
    }
}
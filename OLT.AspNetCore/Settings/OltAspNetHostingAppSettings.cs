namespace OLT.Core
{
    public class OltAspNetHostingAppSettings : IOltAppSettings
    {
        /// <summary>
        /// Preconfigured Hosting Configuration Name
        /// </summary>
        /// <remarks>
        /// Default Value is HostingDefault <seealso cref="OltAspNetDefaults.HostingConfigurations"/>
        /// </remarks>
        public virtual string ConfigurationName { get; set; } = OltAspNetDefaults.HostingConfigurations.Default;

        /// <summary>
        /// Adds middleware for using HSTS, which adds the Strict-Transport-Security header.
        /// </summary>
        public virtual bool UseHsts { get; set; }

        /// <summary>
        /// Adds a middleware that extracts the specified path base from request path and postpend it to the request path base.
        /// </summary>
        public virtual string PathBase { get; set; }


        /// <summary>
        /// Disables middleware for redirecting HTTP Requests to HTTPS.
        /// </summary>
        public virtual bool DisableHttpsRedirect { get; set; }

        /// <summary>
        /// Disabled the add <see cref="AuthenticationMiddleware"/> to the specified <see cref="IApplicationBuilder"/>, which enables authentication capabilities.
        /// </summary>
        public virtual bool DisableUseAuthentication { get; set; }


        /// <summary>
        /// Disabled the add the <see cref="AuthorizationMiddleware"/> to the specified <see cref="IApplicationBuilder"/>, which enables authorization capabilities.
        /// <para>
        /// When authorizing a resource that is routed using endpoint routing, this call must appear between the calls to
        /// <c>app.UseRouting()</c> and <c>app.UseEndpoints(...)</c> for the middleware to function correctly.
        /// </para>
        /// </summary>
        public virtual bool DisableUseAuthorization { get; set; }


    }
}
namespace OLT.Core
{
    public class OltAspNetCoreOptions : OltInjectionOptions
    {
        public bool EnableSwagger { get; set; } = false;
        public bool EnableCores { get; set; } = false;
        public bool DisableNewtonsoftJson { get; set; } = false;
        public string JwtSecret { get; set; }
    }
}
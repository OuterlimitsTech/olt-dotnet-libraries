namespace OLT.Core
{
    public static class OltAspNetDefaults
    {
        public const string CorsPolicyName = "Olt_CorsPolicy_Disabled";

        public static class HostingConfigurations
        {
            public const string Default = "HostingDefault";
            public const string Ngix = "HostingNgix";
            public const string Apache = "HostingApache";
        }
    }
}
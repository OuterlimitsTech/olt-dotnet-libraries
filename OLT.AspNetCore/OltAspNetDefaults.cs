namespace OLT.Core
{
    public static class OltAspNetDefaults
    {
        public const string CorsPolicyName = CorsPolicies.Disabled;

        public static class CorsPolicies
        {
            public const string Disabled = "Ct_CorsPolicy_Disabled";
            public const string Wildcard = "Ct_CorsPolicy_Wildcard";
        }

        public static class HostingConfigurations
        {
            public const string Default = "Olt_HostingDefault";
            public const string Ngix = "Olt_HostingNgix";
            public const string Apache = "Olt_HostingApache";
        }
    }
}
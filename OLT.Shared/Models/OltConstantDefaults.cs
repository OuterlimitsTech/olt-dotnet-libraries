namespace OLT.Core
{
    public static class OltDefaults
    {
        /// <summary>
        /// Default value for AuthenticationScheme property in the JwtBearerAuthenticationOptions
        /// </summary>
        public const string JwtAuthenticationScheme = "Bearer";

        public const string JwtIssuer = "http://jwt.outerlimitstech.com/local";

        public static class CalendarInvite
        {
            public const string FileName = "invite.ics";
            public const string TextCalendar = "text/calendar";
        }


        /// <summary>
        /// Commonly used environment names. (Cloned from Microsoft.Extensions.Hosting.Environments)
        /// </summary>
        public static class OltEnvironments
        {
            public static readonly string Development = "Development";
            public static readonly string Test = "Test";
            public static readonly string Staging = "Staging";
            public static readonly string Production = "Production";
        }

    }



}
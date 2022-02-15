using System;

namespace OLT.Core
{
    public static class OltDefaults 
    {

        public static class Authentication
        {
            public static class Jwt
            {
                /// <summary>
                /// Default value for AuthenticationScheme property in the JwtBearerAuthenticationOptions
                /// </summary>
                public const string AuthenticationScheme = "Bearer";

                /// <summary>
                /// Default issuer for AuthenticationScheme property in the JwtBearerAuthenticationOptions
                /// </summary>
                public const string Issuer = "http://jwt.outerlimitstech.com/local";
            }
        }

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

        public static readonly char[] UpperCase = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static readonly char[] LowerCase = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static readonly char[] Numerals = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static readonly char[] Symbols = { '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '{', '[', '}', ']', '-', '_', '=', '+', ':', ';', '|', '/', '?', ',', '<', '.', '>' };
        public static readonly char[] SpecialCharacters = { '!', '@', '#', '$', '%', '&', '*', '+' };     
    }
}
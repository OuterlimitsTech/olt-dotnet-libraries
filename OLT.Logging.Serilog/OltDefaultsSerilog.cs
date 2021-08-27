using System;
using System.Collections.Generic;
using System.Data;


// ReSharper disable once CheckNamespace
namespace OLT.Logging.Serilog
{

    public static partial class OltDefaultsSerilog
    {

        public static class Properties
        {
            public const string UserPrincipalName = "UserPrincipalName";
            public const string Username = "Username";
            public const string DbUsername = "DbUsername";
            public const string EventType = "OltEventType";
            public const string Environment = "Environment";
            public const string DebuggerAttached = "DebuggerAttached";
        }

        public static class Templates
        {
            public const string DefaultOutput =
                "[{Timestamp:HH:mm:ss} {Level:u3}] {OltEventType:x8} {Message:lj}{NewLine}{Exception}";

            public static class Email
            {
                public static string DefaultEmail => Environment.NewLine +
                                                     Environment.NewLine +
                                                     DefaultOutput +
                                                     Environment.NewLine +
                                                     Environment.NewLine;

                public const string DefaultSubject =
                    "APPLICATION {Level} on {Application} {Environment} Environment occurred at {Timestamp}";
            }
        }
    }
}
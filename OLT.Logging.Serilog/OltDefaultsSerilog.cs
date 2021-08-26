using System;
using System.Collections.Generic;
using System.Data;


// ReSharper disable once CheckNamespace
namespace OLT.Logging.Serilog
{
    public class OltDbColumn
    {
        public string PropertyName { get; set; }
        public string ColumnName { get; set; }
        public int DefaultLength { get; set; }
    }


    public static partial class OltDefaultsSerilog
    {

        public static class Database
        {
            public static List<OltDbColumn> Columns()
            {
                return new List<OltDbColumn>
                {
                    new OltDbColumn
                    {
                        ColumnName = OltDefaultsSerilog.Properties.EventType,
                        PropertyName = OltDefaultsSerilog.Properties.EventType,
                        DefaultLength = 20
                    },
                    new OltDbColumn
                    {
                        ColumnName = OltDefaultsSerilog.Properties.UserPrincipalName,
                        PropertyName = OltDefaultsSerilog.Properties.UserPrincipalName,
                        DefaultLength = 100
                    },
                    new OltDbColumn
                    {
                        ColumnName = OltDefaultsSerilog.Properties.Username,
                        PropertyName = OltDefaultsSerilog.Properties.Username,
                        DefaultLength = 100
                    },
                    new OltDbColumn
                    {
                        ColumnName = OltDefaultsSerilog.Properties.DbUsername,
                        PropertyName = OltDefaultsSerilog.Properties.DbUsername,
                        DefaultLength = 100
                    }
                };
            }
        }

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
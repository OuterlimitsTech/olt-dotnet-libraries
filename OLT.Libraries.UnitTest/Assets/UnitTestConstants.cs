using System;

namespace OLT.Libraries.UnitTest.Assets
{
    // ReSharper disable once InconsistentNaming
    public static class UnitTestConstants
    {
        public static class GuidValues
        {
            public const string String = "5C60F693-BEF5-E011-A485-80EE7300C695";
            public static readonly Guid Value = new Guid(String);

            public const string String2 = "E4EA2B7E-98C1-440E-B347-F075844A9525";
            public static readonly Guid Value2 = new Guid(String2);

            public const string String3 = "3b8c07927ec7445491d6a07ededd2e57";
            public static readonly Guid Value3 = new Guid(String2);
        }

        public static class StringValues
        {
            public const string HelloWorld = "Hello World";
            public const string Hello = "Hello";
            public const string HelloReverse = "olleH";
            public const string World = "World";
            public const string FooBar = "FooBar";
            public const string AlphaNumeric = "AB193C53D";
            public const string Hex = "af8a0f2ba21a7eea22f31dcf693d6efb5bbd58bc";
            public const string ThisIsATest = "This is a Test";
            public const string This = "This";
            public const string Test = "Test";

            static readonly char[] SpecialCharacters = { '!', '@', '#', '$', '%', '&', '*', '+' };

            public static class PhoneValues
            {
                public const string Formatted = "(317) 555-1234";
                public const string Clean = "3175551234";
            }
        }

        public static class DateTimeValues
        {
            public static DateTime Value = new DateTime(2000, 9, 1);
            public const string String = "9/1/2000";
        }

        public static class DecimalValues
        {
            public static decimal Value = 3.1415M;
            public const string String = "3.1415";
        }


        public static class BoolValues
        {
            public static class TrueValues
            {
                public const bool Value = true;
                public const string String = "true";
                public const string Int = "1";

            }
            public static class FalseValues
            {
                public const bool Value = false;
                public const string String = "false";
                public const string Int = "0";
            }
        }

        public static class IntValues
        {
            public static decimal Value = 1234;
            public const string String = "1234";
        }
    }
}
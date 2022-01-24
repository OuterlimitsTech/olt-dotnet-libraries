using System;
using System.Collections.Generic;
using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class StringExtensionTests : BaseTest
    {

        public StringExtensionTests(ITestOutputHelper output) : base(output)
        {
        }


        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        [InlineData(null, null, null)]
        [InlineData(null, "", "")]
        [InlineData(null, "Hello", "Hello")]
        [InlineData("Test", "Hello", "Test")]
        public void GetValueOrDefault(string value, string defaultValue, string expected)
        {
            Assert.Equal(expected, value.GetValueOrDefault(defaultValue));
        }

        [Fact]
        public void RemoveDoubleSpaces()
        {
            var value = $"  {UnitTestConstants.StringValues.HelloWorld}  {UnitTestConstants.StringValues.ThisIsATest}";
            var eval = $"{UnitTestConstants.StringValues.HelloWorld} {UnitTestConstants.StringValues.ThisIsATest}";
            Assert.Equal(value.RemoveDoubleSpaces(), eval);
            Assert.Null(OltStringExtensions.RemoveDoubleSpaces(null));
        }

        [Fact]
        public void CleanForSearch()
        {
            var value = $"   -> ? {UnitTestConstants.StringValues.HelloWorld},   & {UnitTestConstants.StringValues.ThisIsATest}";
            var eval = $"{UnitTestConstants.StringValues.HelloWorld} {UnitTestConstants.StringValues.ThisIsATest}";
            Assert.Equal(value.CleanForSearch(), eval);
            Assert.Null(OltStringExtensions.CleanForSearch(null));
        }


        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData(null, null)]
        [InlineData("!&*$", "")]
        [InlineData("Hello &!There", "Hello There")]
        public void RemoveSpecialCharacters(string value, string expectedResult)
        {
            Assert.Equal(expectedResult, value.RemoveSpecialCharacters());
        }

        [Fact]
        public void ToWords()
        {
            Assert.Collection(UnitTestConstants.StringValues.HelloWorld.ToWords(), 
                item => Assert.Equal(UnitTestConstants.StringValues.Hello, item), 
                item => Assert.Equal(UnitTestConstants.StringValues.World, item));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("John's", "John''s")]
        [InlineData("Johns", "Johns")]
        [InlineData("\"John's\"", "\"John''s\"")]
        public void DuplicateTicksForSql(string value, string expectedResult)
        {
            Assert.Equal(expectedResult, value.DuplicateTicksForSql());
        }

        [Theory]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, 5, UnitTestConstants.StringValues.World)]
        [InlineData("", 10, "")]
        [InlineData(null, 10, null)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, 20, UnitTestConstants.StringValues.HelloWorld)]
        public void Right(string value, int length, string expectedResult)
        {
            Assert.Equal(expectedResult, value.Right(length));
            Assert.Equal(expectedResult, value.Tail(length));
        }

        [Theory]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, 5, UnitTestConstants.StringValues.Hello)]
        [InlineData("", 10, "")]
        [InlineData(null, 10, null)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, 20, UnitTestConstants.StringValues.HelloWorld)]
        public void Left(string value, int length, string expectedResult)
        {
            Assert.Equal(expectedResult, value.Left(length));
            Assert.Equal(expectedResult, value.Head(length));
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(UnitTestConstants.GuidValues.String, true)]
        [InlineData(UnitTestConstants.GuidValues.String2, true)]
        [InlineData(UnitTestConstants.GuidValues.String3, true)]
        [InlineData(UnitTestConstants.StringValues.FooBar, false)]
        [InlineData(UnitTestConstants.StringValues.Hex, false)]
        [InlineData(UnitTestConstants.IntValues.String, false)]
        public void IsGuid(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.IsGuid());
        }


        [Theory]
        [InlineData(UnitTestConstants.GuidValues.String, UnitTestConstants.GuidValues.String)]
        [InlineData(UnitTestConstants.GuidValues.String, UnitTestConstants.GuidValues.String, UnitTestConstants.GuidValues.String2)]
        [InlineData(UnitTestConstants.StringValues.FooBar, null)]
        [InlineData(UnitTestConstants.StringValues.FooBar, UnitTestConstants.GuidValues.String2, UnitTestConstants.GuidValues.String2)]
        [InlineData(UnitTestConstants.StringValues.Hex, null)]
        [InlineData(UnitTestConstants.StringValues.Hex, UnitTestConstants.GuidValues.String, UnitTestConstants.GuidValues.String)]
        [InlineData(UnitTestConstants.IntValues.String, null)]
        public void ToGuid(string value, string expectedResult, string defaultValue = null)
        {
            var eval = defaultValue != null ? value.ToGuid(new Guid(defaultValue)).ToString() : value.ToGuid()?.ToString();
            Assert.Equal(expectedResult?.ToLower(), eval);
        }


        [Theory]
        [InlineData(UnitTestConstants.IntValues.String, true)]
        [InlineData(UnitTestConstants.DecimalValues.String, true)]
        [InlineData(UnitTestConstants.StringValues.FooBar, false)]
        public void IsNumeric(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.IsNumeric());
        }


        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData(UnitTestConstants.StringValues.PhoneValues.Formatted, UnitTestConstants.StringValues.PhoneValues.Clean)]
        public void StripNonNumeric(string value, string expectedResult)
        {
            Assert.Equal(expectedResult, value.StripNonNumeric());
        }


        [Theory]
        [InlineData("", "", true)]
        [InlineData("", "", false)]
        [InlineData(null, null, true)]
        [InlineData(null, null, false)]
        [InlineData(UnitTestConstants.DecimalValues.String, UnitTestConstants.DecimalValues.String, true, UnitTestConstants.StringValues.HelloWorld)]
        [InlineData(UnitTestConstants.DecimalValues.String, "31415", false, UnitTestConstants.StringValues.HelloWorld)]
        public void StripNonNumericDecimal(string value, string expectedResult, bool allowDecimal, string value2 = null)
        {
            if (value == null)
            {
                Assert.Equal(expectedResult, value.StripNonNumeric(allowDecimal));
                return;
            }
            Assert.Equal(expectedResult, $"{value}{value2}".StripNonNumeric(allowDecimal));
        }

        [Theory]
        [InlineData("", "", 10, "")]
        [InlineData(" ", " ", 10, "")]
        [InlineData(null, null, 20, null)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, UnitTestConstants.StringValues.Test, 7, "hello-world-test")]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, UnitTestConstants.StringValues.Test, 100, "hello-world-test")]
        [InlineData("$%hello", UnitTestConstants.StringValues.Test, 100, "hello-test")]
        [InlineData("$%hello-another", UnitTestConstants.StringValues.Test, 100, "hello-another-test")]
        public void Slugify(string value, string value2, int maxLength, string expected)
        {
            var testValue = value == null ? value : $"{value}   ${value2}";
            Assert.Equal(expected.Left(maxLength), testValue.Slugify(maxLength));
        }

        [Theory]
        [InlineData("", "", ", ", "")]
        [InlineData(" ", " ", ", ", " ,  ")]
        [InlineData(null, null, ", ", "")]
        [InlineData(null, "Hello", ", ", "Hello")]
        [InlineData("Hello", "There", " ", "Hello There")]
        [InlineData("Hello", "There", ",", "Hello,There")]
        public void Append(string value, string value2, string separator, string expected)
        {
            Assert.Equal(expected, value.Append(value2, separator));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData(null, null)]
        [InlineData("Hello There", "Hello there")]
        [InlineData("hello there", "Hello there")]
        [InlineData("hello There", "Hello there")]
        [InlineData("HELLO THERE CHARLIE Brown", "Hello there charlie brown")]
        public void ToProperCase(string value, string expected)
        {
            Assert.Equal(expected, value.ToProperCase());
        }



        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData(null, false)]
        [InlineData("9/1/2021", true)]
        [InlineData("2021-09-01", true)]
        public void IsDate(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.IsDate());
        }


        public static IEnumerable<object[]> ToDateMemberData =>
            new List<object[]>
            {
                new object[] { "", null, null },
                new object[] { null, null, null },
                new object[] { "", DateTime.Today, DateTime.Today },
                new object[] { null, DateTime.Today, DateTime.Today },
                new object[] { UnitTestConstants.DateTimeValues.String, UnitTestConstants.DateTimeValues.Value, null },
            };


        [Theory]
        [MemberData(nameof(ToDateMemberData))]
        public void ToDate(string value, DateTime? expectedResult, DateTime? defaultValue = null)
        {
            Assert.Equal(expectedResult, defaultValue.HasValue? value.ToDate(defaultValue.Value) : value.ToDate());
        }



        [Theory]
        [InlineData("", int.MaxValue, int.MaxValue)]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData(" ", 0, 0)]
        [InlineData("FooBar", 0, 0)]
        [InlineData(null, 930248, 930248)]
        [InlineData("45.234", null)]
        [InlineData("-1", -1)]
        public void ToInt(string value, int? expectedResult, int? defaultValue = null)
        {
            Assert.Equal(expectedResult, defaultValue.HasValue ? value.ToInt(defaultValue.Value) : value.ToInt());
        }

        [Fact]
        public void ToIntOverflow()
        {
            long num = int.MaxValue;
            var value = (num + 1).ToString();
            Assert.Null(value.ToInt());
        }

        [Fact]
        public void ToIntDefaultValue()
        {
            int value = int.MaxValue - 100;
            Assert.True(UnitTestConstants.StringValues.HelloWorld.ToInt(value).Equals(value));
        }



        [Theory]
        [InlineData("", long.MaxValue, long.MaxValue)]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData(" ", 0, 0)]
        [InlineData("FooBar", 0, 0)]
        [InlineData(null, long.MaxValue, long.MaxValue)]
        [InlineData("45.234", null)]
        [InlineData("-9223372036854775800", -9223372036854775800, 0)]
        [InlineData("-1", -1)]
        public void ToLong(string value, long? expectedResult, long? defaultValue = null)
        {
            Assert.Equal(expectedResult, defaultValue.HasValue ? value.ToLong(defaultValue.Value) : value.ToLong());
        }


        public static IEnumerable<object[]> ToDecimalMemberData =>
            new List<object[]>
            {
                new object[] { null, decimal.MaxValue, decimal.MaxValue },
                new object[] { "", null },
                new object[] { null, null },
                new object[] { "", 0.0m, 0.0m },
                new object[] { "FooBar", 2.133m, 2.133m },
                new object[] { "45.234", 45.234m, 0.0m },
                new object[] { "35", 35m },
                new object[] { null, 45.234m, 45.234m },
                new object[] { "-1", -1m },
            };


        [Theory]
        [MemberData(nameof(ToDecimalMemberData))]
        public void ToDecimal(string value, decimal? expectedResult, decimal? defaultValue = null)
        {
            Assert.Equal(expectedResult, defaultValue.HasValue ? value.ToDecimal(defaultValue.Value) : value.ToDecimal());
        }

        [Theory]
        [InlineData("", double.MaxValue, double.MaxValue)]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData(" ", 0, 0)]
        [InlineData(null, 1.01, 1.01)]
        [InlineData("FooBar", 0, 0)]
        [InlineData("45.234", 45.234, 0.0)]
        [InlineData("-1", -1)]
        public void ToDouble(string value, double? expectedResult, double? defaultValue = null)
        {
            Assert.Equal(expectedResult, defaultValue.HasValue ? value.ToDouble(defaultValue.Value) : value.ToDouble());
        }


        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData(null, false)]
        [InlineData(UnitTestConstants.BoolValues.TrueValues.String, true)]
        [InlineData(UnitTestConstants.BoolValues.TrueValues.Int, true)]
        [InlineData(UnitTestConstants.StringValues.AlphaNumeric, false)]
        [InlineData(UnitTestConstants.BoolValues.FalseValues.String, true)]
        [InlineData(UnitTestConstants.BoolValues.FalseValues.Int, true)]
        public void IsBool(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.IsBool());
        }

        [Theory]
        [InlineData("", null)]
        [InlineData("", true, true)]
        [InlineData("", false, false)]
        [InlineData(" ", false, false)]
        [InlineData(null, null)]
        [InlineData(null, true, true)]
        [InlineData(null, false, false)]
        [InlineData(UnitTestConstants.BoolValues.TrueValues.String, true)]
        [InlineData(UnitTestConstants.BoolValues.TrueValues.String, true, false)]
        [InlineData(UnitTestConstants.BoolValues.TrueValues.Int, true)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, null)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, true, true)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, false, false)]
        [InlineData(UnitTestConstants.BoolValues.FalseValues.String, false)]
        [InlineData(UnitTestConstants.BoolValues.FalseValues.String, false, true)]
        [InlineData(UnitTestConstants.BoolValues.FalseValues.Int, false)]
        public void ToBool(string value, bool? expectedResult, bool? defaultValue = null)
        {
            Assert.Equal(expectedResult, defaultValue.HasValue ? value.ToBool(defaultValue.Value) : value.ToBool());
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData(null, false)]
        [InlineData("d7980689965b", true)]
        [InlineData("123987", true)]
        [InlineData(UnitTestConstants.StringValues.Hex, true)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, false)]
        public void IsHex(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.IsHex());
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData(UnitTestConstants.StringValues.Hex, true)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, false)]
        public void FromHexToByte(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.FromHexToByte().Length > 0);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData(null, null)]
        [InlineData(UnitTestConstants.StringValues.Hello, UnitTestConstants.StringValues.HelloReverse)]
        [InlineData("1234ABC", "CBA4321")]
        [InlineData(" 1234ABC ", " CBA4321 ")]
        [InlineData(" 1234ABC Testing", "gnitseT CBA4321 ")]
        public void Reverse(string value, string expectedResult)
        {
            Assert.Equal(expectedResult, value.Reverse());
        }

        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData(null, false)]
        [InlineData(UnitTestConstants.StringValues.HelloReverse, false)]
        [InlineData(UnitTestConstants.StringValues.ThisIsATest, true)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, true)]
        public void StartsWithAny(string value, bool expectedResult)
        {
            Assert.Equal(value.StartsWithAny(UnitTestConstants.StringValues.Test, UnitTestConstants.StringValues.Hello, UnitTestConstants.StringValues.This), expectedResult);
        }


        [Theory]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData(null, false)]
        [InlineData(UnitTestConstants.StringValues.HelloReverse, false)]
        [InlineData(UnitTestConstants.StringValues.ThisIsATest, true)]
        public void EqualsAny(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.EqualsAny(UnitTestConstants.StringValues.ThisIsATest, UnitTestConstants.StringValues.Hex));
        }

        [Theory]
        [InlineData("", true)]
        [InlineData(UnitTestConstants.StringValues.Hello, false)]
        public void DBNullIfEmpty(string value, bool expectedResult)
        {
            Assert.Equal(value.DBNullIfEmpty().Equals(DBNull.Value), expectedResult);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("Hello There")]
        [InlineData("HelloThere")]
        public void Base64EncodeDecode(string value)
        {
            var encoded = value.Base64Encode();
            Assert.Equal(value, encoded.Base64Decode());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData(null, null)]
        [InlineData("Hello There", "Hello There")]
        [InlineData("HelloThere", "Hello there")]
        [InlineData("HelloThereSally", "Hello there sally")]
        public void ToSentenceCase(string value, string expectedResult)
        {
            Assert.Equal(expectedResult, value.ToSentenceCase());
        }
        

        [Theory]
        [InlineData(16, true, true, true, true, 16)]
        [InlineData(12, false, true, true, true, 12)]
        [InlineData(13, true, false, true, true, 13)]
        [InlineData(18, true, true, false, true, 18)]
        [InlineData(24, true, true, true, false, 24)]
        [InlineData(8, false, false, false, false, 0)]
        public void GeneratePassword(int length, bool useNumbers, bool useLowerCaseLetters, bool useUpperCaseLetters, bool useSymbols, int expectedLength)
        {
            if (expectedLength > 0)
            {
                Assert.True(OltKeyGenerator.GeneratePassword(length, useNumbers, useLowerCaseLetters, useUpperCaseLetters, useSymbols).Length == expectedLength);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => OltKeyGenerator.GeneratePassword(length, useNumbers, useLowerCaseLetters, useUpperCaseLetters, useSymbols));
            }
        }


        [Theory]
        [InlineData(0)]
        [InlineData(25)]
        [InlineData(10)]
        public void GetUniqueKey(int size)
        {
            var password = OltKeyGenerator.GetUniqueKey(size);
            Assert.True(password.Length == size);
        }
        
        [Theory]
        [InlineData(1000000, 32, 2.0)]
        [InlineData(1000000, 64, 2.0)]
        public void GetUniqueKeyBiasedIteration(int repetitions, int keySize, double threshold)
        {
            Logger.Debug("Original BIASED implementation");
            var result = KeyGeneratorPerformTest(repetitions, keySize, OltKeyGenerator.GetUniqueKeyOriginal_BIASED);
            Assert.DoesNotContain(result, p => p.Value > threshold);
        }

        [Theory]
        [InlineData(1000000, 32, 1.65)]
        [InlineData(1000000, 64, 1.65)]
        public void GetUniqueKeyIteration(int repetitions, int keySize, double threshold)
        {
            Logger.Debug("Original implementation");
            var result = KeyGeneratorPerformTest(repetitions, keySize, OltKeyGenerator.GetUniqueKey);
            Assert.DoesNotContain(result, p => p.Value > threshold);
        }

        private Dictionary<char, double> KeyGeneratorPerformTest(int repetitions, int keySize, Func<int, string> generator)
        {
            var result = new Dictionary<char, double>();
            var chars = (new char[0])
                .Concat(OltDefaults.UpperCase)
                .Concat(OltDefaults.LowerCase)
                .Concat(OltDefaults.Numerals)
                .ToArray();

            Dictionary<char, int> counts = new Dictionary<char, int>();
            foreach (var ch in chars) counts.Add(ch, 0);

            for (int i = 0; i < repetitions; i++)
            {
                var key = generator(keySize);
                foreach (var ch in key) counts[ch]++;
            }

            int totalChars = counts.Values.Sum();
            foreach (var ch in chars)
            {
                var val = 100.0 * counts[ch] / totalChars;
                result.Add(ch, val);
                Logger.Debug($"{ch}: {val:#.000}%");
            }

            return result;
        }



    }
}
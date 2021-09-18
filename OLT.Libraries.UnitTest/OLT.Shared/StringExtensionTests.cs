using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.Shared
{
    public class StringExtensionTests : BaseTest
    {

        public StringExtensionTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CleanForSearch()
        {
            var value = $"   -> ? {UnitTestConstants.StringValues.HelloWorld},   & {UnitTestConstants.StringValues.ThisIsATest}";
            var eval = $"{UnitTestConstants.StringValues.HelloWorld} {UnitTestConstants.StringValues.ThisIsATest}";
            Assert.True(value.CleanForSearch().Equals(eval), $"|{value.CleanForSearch()}|  |{eval}|");
        }

        [Fact]
        public void ToWords()
        {
            Assert.Collection(UnitTestConstants.StringValues.HelloWorld.ToWords(), 
                item => Assert.Equal(UnitTestConstants.StringValues.Hello, item), 
                item => Assert.Equal(UnitTestConstants.StringValues.World, item));
        }

        [Theory]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, 5, UnitTestConstants.StringValues.World)]
        [InlineData("", 10, "")]
        [InlineData(null, 10, null)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, 20, UnitTestConstants.StringValues.HelloWorld)]
        public void Right(string value, int length, string expectedResult)
        {
            Assert.Equal(expectedResult, value.Right(length));
        }

        [Theory]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, 5, UnitTestConstants.StringValues.Hello)]
        [InlineData("", 10, "")]
        [InlineData(null, 10, null)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, 20, UnitTestConstants.StringValues.HelloWorld)]
        public void Left(string value, int length, string expectedResult)
        {
            Assert.Equal(expectedResult, value.Left(length));
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
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData(UnitTestConstants.DecimalValues.String, UnitTestConstants.DecimalValues.String, UnitTestConstants.StringValues.HelloWorld)]
        public void StripNonNumericDecimal(string value, string expectedResult, string value2 = null)
        {
            if (value == null)
            {
                Assert.Equal(expectedResult, value.StripNonNumeric(true));
                return;
            }
            Assert.Equal(expectedResult, $"{value}{value2}".StripNonNumeric(true));
        }

        [Theory]
        [InlineData("", "", 10, "")]
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


        [Fact]
        public void ToDate()
        {
            Assert.True(UnitTestConstants.DateTimeValues.String.ToDate().Equals(UnitTestConstants.DateTimeValues.Value));
        }



        [Fact]
        public void ToInt()
        {
            int value = int.MaxValue - 100;
            Assert.True(value.ToString().ToInt().Equals(value));
        }

        [Fact]
        public void ToIntDefaultValue()
        {
            int value = int.MaxValue - 100;
            Assert.True(UnitTestConstants.StringValues.HelloWorld.ToInt(value).Equals(value));
        }


        [Fact]
        public void ToLong()
        {
            long value = long.MaxValue - 500;
            Assert.True(value.ToString().ToLong().Equals(value));
        }

        [Fact]
        public void ToLongDefaultValue()
        {
            long value = long.MaxValue - 500;
            Assert.True(UnitTestConstants.StringValues.HelloWorld.ToLong(value).Equals(value));
        }


        [Fact]
        public void ToDecimal()
        {
            decimal value = decimal.MaxValue - 500;
            Assert.True(value.ToString().ToDecimal().Equals(value));
        }

        [Fact]
        public void ToDecimalDefaultValue()
        {
            decimal value = decimal.MaxValue - 500;
            Assert.True(UnitTestConstants.StringValues.HelloWorld.ToDecimal(value).Equals(value));
        }


        [Fact]
        public void ToDouble()
        {
            double value = double.MaxValue - 500;
            Assert.True(value.ToString().ToDouble().Equals(value));
        }

        [Fact]
        public void ToDoubleDefaultValue()
        {
            double value = double.MaxValue - 500;
            Assert.True(UnitTestConstants.StringValues.HelloWorld.ToDouble(value).Equals(value));
        }


        [Theory]
        [InlineData(UnitTestConstants.BoolValues.TrueValues.String, true)]
        [InlineData(UnitTestConstants.BoolValues.TrueValues.Int, true)]
        [InlineData(UnitTestConstants.StringValues.AlphaNumeric, false)]
        [InlineData(UnitTestConstants.BoolValues.FalseValues.String, true)]
        [InlineData(UnitTestConstants.BoolValues.FalseValues.Int, true)]
        public void IsBool(string value, bool expectedResult)
        {
            Assert.Equal(value.IsBool(), expectedResult);
        }

        [Theory]
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
            if (defaultValue.HasValue)
            {
                Assert.Equal(value.ToBool(defaultValue.Value), expectedResult);
                return;
            }
            Assert.Equal(value.ToBool(), expectedResult);
        }


        [Theory]
        [InlineData(UnitTestConstants.StringValues.Hex, true)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, false)]
        public void FromHexToByte(string value, bool expectedResult)
        {
            Assert.Equal(value.FromHexToByte().Length > 0, expectedResult);
        }

        [Fact]
        public void Reverse()
        {
            Assert.True(UnitTestConstants.StringValues.Hello.Reverse().Equals(UnitTestConstants.StringValues.HelloReverse));
        }

        [Theory]
        [InlineData(UnitTestConstants.StringValues.HelloReverse, false)]
        [InlineData(UnitTestConstants.StringValues.ThisIsATest, true)]
        [InlineData(UnitTestConstants.StringValues.HelloWorld, true)]
        public void StartsWithAny(string value, bool expectedResult)
        {
            Assert.Equal(value.StartsWithAny(UnitTestConstants.StringValues.Test, UnitTestConstants.StringValues.Hello, UnitTestConstants.StringValues.This), expectedResult);
        }


        [Theory]
        [InlineData(UnitTestConstants.StringValues.HelloReverse, false)]
        [InlineData(UnitTestConstants.StringValues.ThisIsATest, true)]
        public void EqualsAny(string value, bool expectedResult)
        {
            Assert.Equal(value.EqualsAny(UnitTestConstants.StringValues.ThisIsATest, UnitTestConstants.StringValues.Hex), expectedResult);
        }

        [Theory]
        [InlineData("", true)]
        [InlineData(UnitTestConstants.StringValues.Hello, false)]
        public void DBNullIfEmpty(string value, bool expectedResult)
        {
            Assert.Equal(value.DBNullIfEmpty().Equals(DBNull.Value), expectedResult);
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
            var result = OltKeyGeneratorPerformTest(repetitions, keySize, OltKeyGenerator.GetUniqueKeyOriginal_BIASED);
            Assert.DoesNotContain(result, p => p.Value > threshold);
        }

        [Theory]
        [InlineData(1000000, 32, 1.65)]
        [InlineData(1000000, 64, 1.65)]
        public void GetUniqueKeyIteration(int repetitions, int keySize, double threshold)
        {
            Logger.Debug("Original implementation");
            var result = OltKeyGeneratorPerformTest(repetitions, keySize, OltKeyGenerator.GetUniqueKey);
            Assert.DoesNotContain(result, p => p.Value > threshold);
        }

        private Dictionary<char, double> OltKeyGeneratorPerformTest(int repetitions, int keySize, Func<int, string> generator)
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
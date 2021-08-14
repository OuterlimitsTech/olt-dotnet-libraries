using System;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Shared
{
    public class StringExtensionTests : OltDisposable
    {

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

        [Fact]
        public void Right()
        {
            Assert.True(UnitTestConstants.StringValues.HelloWorld.Right(5).Equals(UnitTestConstants.StringValues.World));
        }

        [Fact]
        public void Left()
        {
            Assert.True(UnitTestConstants.StringValues.HelloWorld.Left(5).Equals(UnitTestConstants.StringValues.Hello));
        }

        [Theory]
        [InlineData(UnitTestConstants.GuidValues.String, true)]
        [InlineData(UnitTestConstants.GuidValues.String2, true)]
        [InlineData(UnitTestConstants.GuidValues.String3, true)]
        [InlineData(UnitTestConstants.StringValues.FooBar, false)]
        [InlineData(UnitTestConstants.StringValues.Hex, false)]
        [InlineData(UnitTestConstants.IntValues.String, false)]
        public void IsGuid(string value, bool expectedResult)
        {
            Assert.Equal(value.IsGuid(), expectedResult);
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
            Assert.Equal(eval, expectedResult?.ToLower());
        }


        [Theory]
        [InlineData(UnitTestConstants.IntValues.String, true)]
        [InlineData(UnitTestConstants.DecimalValues.String, true)]
        [InlineData(UnitTestConstants.StringValues.FooBar, false)]
        public void IsNumeric(string value, bool expectedResult)
        {
            Assert.Equal(value.IsNumeric(), expectedResult);
        }


        [Fact]
        public void StripNonNumeric()
        {
            Assert.True(UnitTestConstants.StringValues.PhoneValues.Formatted.StripNonNumeric().Equals(UnitTestConstants.StringValues.PhoneValues.Clean), 
                $"{UnitTestConstants.StringValues.PhoneValues.Formatted.StripNonNumeric()} <> {UnitTestConstants.StringValues.PhoneValues.Clean}");
        }

        [Fact]
        public void StripNonNumericDecimal()
        {
            var value = $"{UnitTestConstants.DecimalValues.String} {UnitTestConstants.StringValues.HelloWorld}";
            Assert.True(value.StripNonNumeric(true).Equals(UnitTestConstants.DecimalValues.String));
        }

        [Fact]
        public void Slugify()
        {
            var value = $"{UnitTestConstants.StringValues.HelloWorld}   ${UnitTestConstants.StringValues.Test}";
            Assert.True(value.Slugify().Equals($"{UnitTestConstants.StringValues.Hello.ToLower()}-{UnitTestConstants.StringValues.World.ToLower()}-{UnitTestConstants.StringValues.Test.ToLower()}"));
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

        [Fact]
        public void GeneratePassword()
        {
            var password = OltKeyGenerator.GeneratePassword(16);
            Assert.True(password.Length == 16);
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

    }
}
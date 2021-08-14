using System;
using OLT.Core;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Shared
{
    public class StringExtensionTests : OltDisposable
    {

        [Fact]
        public void CleanForSearch()
        {
            var name = "Mouse, Mickey   & Duck, Daffy";
            Assert.True(name.CleanForSearch().Equals("Mouse Mickey Duck Daffy"));
        }

        [Fact]
        public void ToWords()
        {
            var value = "Hello World";
            Assert.Collection(value.ToWords(), item => Assert.Equal("Hello", item), item => Assert.Equal("World", item));
        }

        [Fact]
        public void Right()
        {
            var value = "Hello World";
            Assert.True(value.Right(5).Equals("World"));
        }

        [Fact]
        public void Left()
        {
            var value = "Hello World";
            Assert.True(value.Left(5).Equals("Hello"));
        }

        [Fact]
        public void IsGuid()
        {
            var value = Guid.NewGuid().ToString();
            Assert.True(value.IsGuid());
        }

        [Fact]
        public void IsNotGuid()
        {
            var value = "FooBar";
            Assert.False(value.IsGuid());
        }

        [Fact]
        public void IsNumeric()
        {
            var value = "1234";
            Assert.True(value.IsNumeric());
        }

        [Fact]
        public void IsNotNumeric()
        {
            var value = "FooBar";
            Assert.False(value.IsNumeric());
        }

        [Fact]
        public void StripNonNumeric()
        {
            var value = "(317) 555-1234";
            var result = "3175551234";
            Assert.True(value.StripNonNumeric().Equals(result), $"{value.StripNonNumeric()} <> {result}");
        }

        [Fact]
        public void StripNonNumericDecimal()
        {
            var value = "3.1415ABC";
            Assert.True(value.StripNonNumeric(true).Equals("3.1415"));
        }

        [Fact]
        public void Slugify()
        {
            var value = "Hello World   Test";
            Assert.True(value.Slugify().Equals("hello-world-test"));
        }


        [Fact]
        public void ToDate()
        {
            var value = "9/1/2000";
            var result = new DateTime(2000, 9, 1);
            Assert.True(value.ToDate().Equals(result));
        }

        [Fact]
        public void ToGuid()
        {
            var value = Guid.NewGuid();
            Assert.True(value.ToString().ToGuid().Equals(value));
        }

        [Fact]
        public void ToGuidDefaultValue()
        {
            var value = Guid.NewGuid();
            Assert.True("Hello World".ToGuid(value).Equals(value));
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
            Assert.True("Hello World".ToInt(value).Equals(value));
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
            Assert.True("Hello World".ToLong(value).Equals(value));
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
            Assert.True("Hello World".ToDecimal(value).Equals(value));
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
            Assert.True("Hello World".ToDouble(value).Equals(value));
        }


        [Fact]
        public void IsBool()
        {
            var value = "true";
            Assert.True(value.ToBool());
        }

        [Fact]
        public void IsNotBool()
        {
            var value = "ABC1234";
            Assert.Null(value.ToBool());
        }

        [Fact]
        public void ToTrueBool()
        {
            var value = "true";
            Assert.True(value.ToBool());
        }

        [Fact]
        public void ToTrueBoolDefaultValue()
        {
            Assert.True("Hello World".ToBool(true));
        }

        [Fact]
        public void ToFalseBool()
        {
            var value = "false";
            Assert.False(value.ToBool());
        }

        [Fact]
        public void ToFalseBoolDefaultValue()
        {
            Assert.False("Hello World".ToBool(false));
        }

        [Fact]
        public void FromHexToByte()
        {
            var value = "af8a0f2ba21a7eea22f31dcf693d6efb";
            Assert.NotEmpty(value.FromHexToByte());
        }

        [Fact]
        public void Reverse()
        {
            var value = "Hello";
            Assert.True(value.Reverse().Equals("olleH"));
        }

        [Fact]
        public void StartsWithAny()
        {
            var value = "This is a Test";
            Assert.True(value.StartsWithAny("This", "Hello"));
        }


        [Fact]
        public void EqualsAny()
        {
            var value = "This is a Hello";
            Assert.True(value.EqualsAny("This is a Hello", "Mickey Mouse"));
        }

        [Fact]
        public void DBNullIfEmpty()
        {
            var value = string.Empty;
            Assert.True(value.DBNullIfEmpty().Equals(DBNull.Value));
        }

        [Fact]
        public void DBNullIfEmptyFalse()
        {
            var value = "Hello";
            Assert.False(value.DBNullIfEmpty().Equals(DBNull.Value));
        }

    }
}
using System;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class IntExtensionsTests
    {
        [Theory]
        [InlineData(34, 34.0)]
        public void ToDollars(int value, double expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDollars());
        }

        [Theory]
        [InlineData(34, 34.0)]
        [InlineData(null, null)]
        public void ToDollarsNullable(int? value, double? expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDollars());
        }


        [Theory]
        [InlineData(34, 34.0)]
        public void ToDouble(int value, double expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDouble());
        }

        [Theory]
        [InlineData(34, 34.0)]
        [InlineData(null, null)]
        public void ToDoubleNullable(int? value, double? expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDouble());
        }


        [Theory]
        [InlineData(-1, "-1")]
        [InlineData(0, "0")]
        [InlineData(1, "1st")]
        [InlineData(2, "2nd")]
        [InlineData(3, "3rd")]
        [InlineData(5, "5th")]
        [InlineData(34, "34th")]
        [InlineData(101, "101st")]
        public void AddOrdinal(int value, string expectedResult)
        {
            Assert.Equal(expectedResult, value.AddOrdinal());
        }
    }
}
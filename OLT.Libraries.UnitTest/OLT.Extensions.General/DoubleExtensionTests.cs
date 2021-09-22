using System;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class DoubleExtensionTests
    {
        [Theory]
        [InlineData(34.559, 34.56)]
        [InlineData(34.554, 34.55)]
        [InlineData(34.5545, 34.55)]
        [InlineData(34.5549, 34.55)]
        [InlineData(34, 34)]
        public void ToDollars(double value, double expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDollars());
        }

        [Theory]
        [InlineData(34.559, 34.56)]
        [InlineData(34.554, 34.55)]
        [InlineData(34.5545, 34.55)]
        [InlineData(34.5549, 34.55)]
        [InlineData(34, 34)]
        [InlineData(null, null)]
        public void ToDollarsNullable(double? value, double? expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDollars());
        }
    }
}
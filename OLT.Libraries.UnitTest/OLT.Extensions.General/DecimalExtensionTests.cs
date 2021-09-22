using System;
using System.Collections.Generic;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class DecimalExtensionTests
    {


        #region [ To Dollars ]


        public static IEnumerable<object[]> ToDollarsMemberData =>
            new List<object[]>
            {
                new object[] { 34.559m, 34.56m },
                new object[] { 34.554m, 34.55m },
                new object[] { 34.5549m, 34.55m },
                new object[] { 34m, 34m },
            };


        [Theory]
        [MemberData(nameof(ToDollarsMemberData))]
        public void ToDollars(decimal value, decimal expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDollars());
        }

        public static IEnumerable<object[]> ToDollarsNullableMemberData =>
            new List<object[]>
            {
                new object[] { null, null },
                new object[] { 34.559m, 34.56m },
                new object[] { 34.554m, 34.55m },
                new object[] { 34.5549m, 34.55m },
                new object[] { 34m, 34m },
            };


        [Theory]
        [MemberData(nameof(ToDollarsNullableMemberData))]
        public void ToDollarsNullable(decimal? value, decimal? expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDollars());
        }

        #endregion


        #region [ ToDouble ]

        public static IEnumerable<object[]> ToDoubleMemberData =>
            new List<object[]>
            {
                new object[] { 34.559m, 34.559 },
                new object[] { 34.554m, 34.554 },
                new object[] { 34.5549m, 34.5549 },
                new object[] { 34m, 34.0 },
            };


        [Theory]
        [MemberData(nameof(ToDoubleMemberData))]
        public void ToDouble(decimal value, double expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDouble());
        }

        public static IEnumerable<object[]> ToDoubleNullableMemberData =>
            new List<object[]>
            {
                new object[] { null, null },
                new object[] { 34.559m, 34.559 },
                new object[] { 34.554m, 34.554 },
                new object[] { 34.5549m, 34.5549 },
                new object[] { 34m, 34.0 },
            };


        [Theory]
        [MemberData(nameof(ToDoubleNullableMemberData))]
        public void ToDoubleNullable(decimal? value, double? expectedResult)
        {
            Assert.Equal(expectedResult, value.ToDouble());
        }

        #endregion
    }
}
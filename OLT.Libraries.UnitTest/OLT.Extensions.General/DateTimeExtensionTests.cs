using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class DateTimeExtensionTests
    {
        public static IEnumerable<object[]> DayNumberSuffixData =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 6, 1), "1st" },
                new object[] { new DateTime(2020, 11, 2), "2nd" },
                new object[] { new DateTime(2020, 2, 3), "3rd" },
                new object[] { new DateTime(2020, 10, 22), "22nd" },
                new object[] { new DateTime(2020, 9, 23), "23rd" },
                new object[] { new DateTime(2020, 11, 20), "20th" },
                new object[] { new DateTime(2020, 11, 30), "30th" },
                new object[] { new DateTime(2020, 12, 31), "31st" },
            };

        [Theory]
        [MemberData(nameof(DayNumberSuffixData))]
        public void DayNumberSuffix(DateTime value, string expectedResult)
        {
            Assert.Equal(expectedResult, value.GetDayNumberSuffix());
        }


        public static IEnumerable<object[]> CalculateAgeData =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 1, 31) },
                new object[] { new DateTime(2020, 2, 29) },
                new object[] { DateTime.Today },
                new object[] { DateTime.Today.AddDays(-1) },
                new object[] { DateTime.Today.AddDays(1) },
                new object[] { DateTime.Today.AddDays(50) },
                new object[] { DateTime.Today.AddDays(-3).AddMonths(-18).AddYears(-75) },
            };

        [Theory]
        [MemberData(nameof(CalculateAgeData))]
        public void CalculateAge(DateTime birthDate)
        {
            var expectedResult = birthDate > DateTime.Today ? 0 : (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.25D);
            Assert.Equal(expectedResult, birthDate.CalculateAge());
        }

        public static IEnumerable<object[]> ToISO8601Data =>
            new List<object[]>
            {
                new object[] { new DateTime(2020, 2, 8) },
                new object[] { DateTimeOffset.Now.AddMonths(-3),  },
            };

        [Theory]
        [MemberData(nameof(ToISO8601Data))]
        public void ToISO8601(DateTimeOffset value)
        {
            var expectedResult = value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            Assert.Equal(expectedResult, value.ToISO8601());
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Models;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.SqlServer
{
    public class FullTextSearchUtil
    {

        private static string FormatResult(string search, OltFtsWildCardType wildCardType, bool matchAllWords)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return search;
            }

            search = search.CleanForSearch();
            var words = search.Split(' ', '　', StringSplitOptions.None).ToList();

            return matchAllWords
                ? string.Join(" and ", words.Where(c => c != "and").Select(word => OltFullTextSearchUtil.FormatWordWildCard(word, wildCardType)))
                : string.Join(" or ", words.Where(c => c != "or").Select(word => OltFullTextSearchUtil.FormatWordWildCard(word, wildCardType)));
        }

        [Theory]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name1.First, OltFtsWildCardType.BeginsWith, false, true)]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name2.First, OltFtsWildCardType.BeginsWith, true, true)]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name2.FullName, OltFtsWildCardType.BeginsWith, false, true)]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name1.First, OltFtsWildCardType.EndsWith, false, true)]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name2.First, OltFtsWildCardType.EndsWith, true, true)]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name2.FullName, OltFtsWildCardType.EndsWith, false, true)]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name1.First, OltFtsWildCardType.Contains, false, true)]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name2.First, OltFtsWildCardType.Contains, true, true)]
        [InlineData(UnitTestConstants.StringValues.PersonNames.Name2.FullName, OltFtsWildCardType.Contains, false, true)]
        [InlineData(null, OltFtsWildCardType.Contains, false, true)]
        [InlineData("", OltFtsWildCardType.Contains, false, true)]
        [InlineData(" ", OltFtsWildCardType.Contains, false, true)]
        public void WildCardType(string value, OltFtsWildCardType widCardType, bool matchAllWords, bool expectedResult)
        {
            var result = OltFullTextSearchUtil.Contains(value, widCardType);
            var compare = FormatResult(value, widCardType, matchAllWords);

            if (expectedResult)
            {
                Assert.Equal(result, compare);
            }
            else
            {
                Assert.NotEqual(result, compare);
            }
        }

        [Fact]
        public void Invalid()
        {
            var word = Faker.Name.First();
            var invalidValue = -1000;
            Assert.Throws<InvalidEnumArgumentException>(() => OltFullTextSearchUtil.Contains(word, (OltFtsWildCardType)invalidValue));
        }

        [Fact]
        public void FreeText()
        {
            var word = Faker.Name.First();
            var firstName = OltFullTextSearchUtil.FreeText(word);
            Assert.True(firstName.Equals(word.CleanForSearch()));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Libraries.UnitTest.OLT.Shared;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Extensions.General
{
    public class CollectionExtensionsTests
    {
        private readonly IPersonService _personService;

        public CollectionExtensionsTests(IPersonService personService)
        {
            _personService = personService;
        }

        public static IEnumerable<object[]> JoinMemberData =>
            new List<object[]>
            {
                new object[] { new List<string> { "Item1", "Item2" }, ",", "Item1,Item2" },
                new object[] { new List<string> { null, "Item2" }, ",", ",Item2" },
                new object[] { new List<string> { "Item1", "Item2" }, null, "Item1Item2" },
                new object[] { new List<string> { "Item1", "Item2" }, "", "Item1Item2" },
            };

        [Theory]
        [MemberData(nameof(JoinMemberData))]
        public void Join(List<string> values, string separator, string expectedResult)
        {
            Assert.Equal(expectedResult, values.Join(separator));
        }


        public static IEnumerable<object[]> DelimitedStringData =>
            new List<object[]>
            {
                new object[] { new HelperToDelimitedString(",", false, "", "Item1", "Item2") },
                new object[] { new HelperToDelimitedString(",", true, "", "Item1", "Item2") },
                new object[] { new HelperToDelimitedString(",", true, "\"", "Item1", "Item2") },
                new object[] { new HelperToDelimitedString(",", false, "\"", "Item1", "Item2") },
                new object[] { new HelperToDelimitedString(":", true, "", "Item1", "Item2", "Item3", "Item4") },
                new object[] { new HelperToDelimitedString(":", false, "'", "Item1", "Item2", "Item6", "Item7", "Item9") },
                new object[] { new HelperToDelimitedString(null, true, "", "Item1", "Item2", "Item3", "Item4") },
                new object[] { new HelperToDelimitedString("-", true, null, "Item1", "Item2", "Item6", "Item7", "Item9") },
                new object[] { new HelperToDelimitedString(",", false, "") },
            };


        [Theory]
        [MemberData(nameof(DelimitedStringData))]
        public void DelimitedString(HelperToDelimitedString request)
        {
            Assert.Equal(request.Expected, request.Values.ToDelimitedString(request.Delimiter, request.InsertSpaces, request.Qualifier));
        }


        [Theory]
        [InlineData(5)]
        [InlineData(500)]
        public async Task ForEachAsyncTest(int iterations)
        {
            var expected = new List<PersonDto>();
            var values = new List<PersonDto>();
            for (var idx = 1; idx <= iterations; idx++)
            {
                values.Add(_personService.Add(UnitTestHelper.CreatePersonDto()));
            }
            
            await values.ForEachAsync(async value =>
            {
                var item = await _personService.GetAsync<PersonDto>(value.PersonId.GetValueOrDefault());  //Only doing this to test await/async
                expected.Add(item);
            });
            values.Should().BeEquivalentTo(expected);
        }

    }
}
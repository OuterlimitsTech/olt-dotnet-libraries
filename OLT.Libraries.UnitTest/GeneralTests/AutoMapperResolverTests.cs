using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.GeneralTests
{
    public class AutoMapperResolverTests : BaseTest
    {
        private readonly IOltAdapterResolverAutoMapper _adapterResolver;
        private readonly SqlDatabaseContext _context;

        public AutoMapperResolverTests(
            SqlDatabaseContext context,
            IOltAdapterResolverAutoMapper adapterResolver,
            ITestOutputHelper output) : base(output)
        {
            _adapterResolver = adapterResolver;
            _context = context;
            SeedPeople(_context);
            SeedUsers(_context);
        }



        [Fact]
        public void Enumerable()
        {
            var list = new List<PersonAutoMapperModel>();
            for (int i = 0; i < Faker.RandomNumber.Next(10, 35); i++)
            {
                list.Add(UnitTestHelper.CreateTestAutoMapperModel());
            }
            var mapped = _adapterResolver.Map<PersonAutoMapperModel, PersonEntity>(list);
            mapped.Should().Equal(list, (c1, c2) => c1.NameFirst == c2.Name.First && c1.NameLast == c2.Name.Last);
        }

        [Fact]
        public void EnumerableReverse()
        {
            var list = new List<PersonEntity>();
            for (int i = 0; i < Faker.RandomNumber.Next(10, 35); i++)
            {
                list.Add(PersonEntity.FakerEntity());
            }
            var mapped = _adapterResolver.Map<PersonEntity, PersonAutoMapperModel>(list).OrderBy(p => p.Name.First).ThenBy(p => p.Name.Last);
            mapped.Should().Equal(list.OrderBy(p => p.NameFirst).ThenBy(p => p.NameLast), (c1, c2) => c1.Name.First == c2.NameFirst && c1.Name.Last == c2.NameLast);
        }

        [Fact]
        public void CanProjectTo()
        {
            Assert.True(_adapterResolver.CanProjectTo<PersonEntity, PersonAutoMapperDto>());
        }

        [Fact]
        public void CantProjectTo()
        {
            Assert.False(_adapterResolver.CanProjectTo<AddressEntity, PersonAutoMapperModel>());
        }


        [Fact]
        public void PagedAdapter()
        {
            var queryable = _context.People.Where(new OltSearcherGetAll<PersonEntity>());
            var paged = _adapterResolver.Paged<PersonEntity, PersonAutoMapperDto>(queryable, new OltPagingParams { Page = 1, Size = 25 });
            Assert.Equal(paged.Data.Count(), paged.Size);
        }

        [Fact]
        public void PagedAdapterOrderBy()
        {
            var queryable = _context.People.Where(new OltSearcherGetAll<PersonEntity>());
            var paged = _adapterResolver.Paged<PersonEntity, PersonAutoMapperDto>(queryable, new OltPagingParams { Page = 1, Size = 25 }, query => query.OrderBy(p => p.NameFirst).ThenBy(p => p.NameLast).ThenBy(p => p.Id));
            Assert.Equal(paged.Data.Count(), paged.Size);
        }

        [Fact]
        public void DefaultOrderBy()
        {
            var orderByFunc = _adapterResolver.DefaultOrderBy<PersonEntity, PersonAutoMapperDto>();
            var queryable = _context.People.Where(new OltSearcherGetAll<PersonEntity>());
            var paged = orderByFunc(queryable).ToPaged(new OltPagingParams { Page = 1, Size = 25 });
            Assert.Equal(paged.Data.Count(), paged.Size);
        }
    }
}
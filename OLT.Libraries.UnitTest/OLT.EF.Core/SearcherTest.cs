using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Libraries.UnitTest.Assets.Searchers;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core
{
    public class SearcherTest : BaseTest
    {
        private readonly IPersonService2 _personService2;
        private readonly SqlDatabaseContext _context;
        private readonly SqlDatabaseContext2 _context2;

        public SearcherTest(
            IPersonService2 personService2,
            SqlDatabaseContext context,
            SqlDatabaseContext2 context2,
            ITestOutputHelper output) : base(output)
        {
            _personService2 = personService2;
            _context = context;
            _context2 = context2;
        }




        [Fact]
        public void WhereSearcher()
        {
            var entity = PersonEntity.FakerEntity();
            _context.People.Add(entity);
            _context.SaveChanges();
            Assert.True(_context.People.Where(new PersonLastNameStartsWithSearcher(entity.NameLast.Left(1))).Any());
        }

        [Fact]
        public void WhereSearcherParams()
        {
            var entity = PersonEntity.FakerEntity();
            _context.People.Add(entity);
            _context.SaveChanges();
            Assert.True(_context.People.Where(new PersonLastNameStartsWithSearcher(entity.NameLast.Left(1)), new PersonFirstNameStartsWithSearcher(entity.NameFirst.Left(1))).Any());
        }


        [Fact]
        public void NoGlobalFilter_OltSearcherGetAll()
        {
            var entity = PersonEntity.FakerEntity();
            _context2.People.Add(entity);
            var deletedEntity = PersonEntity.FakerEntity();
            deletedEntity.DeletedOn = DateTimeOffset.Now;
            _context2.People.Add(deletedEntity);
            _context2.SaveChanges();
            var allNonDeleted = _personService2.Count(new OltSearcherGetAll<PersonEntity>(false));
            var all = _personService2.Count(new OltSearcherGetAll<PersonEntity>(true));
            Assert.True(allNonDeleted < all);
        }

        [Fact]
        public void NoGlobalFilter_OltSearcherGetById()
        {
            var entity = PersonEntity.FakerEntity();
            _context2.People.Add(entity);
            var deletedEntity = PersonEntity.FakerEntity();
            deletedEntity.DeletedOn = DateTimeOffset.Now;
            _context2.People.Add(deletedEntity);
            _context2.SaveChanges();
            var nonDeleted = _personService2.Get<PersonDto>(new OltSearcherGetById<PersonEntity>(entity.Id));
            var deleted = _personService2.Get<PersonDto>(new OltSearcherGetById<PersonEntity>(deletedEntity.Id));
            Assert.True(deleted.PersonId > 0 && nonDeleted.PersonId > 0);
        }

        [Fact]
        public void OrderByPropertyName()
        {
            var list = new List<PersonEntity>()
            {
                new PersonEntity
                {
                    NameFirst = "Todd",
                    NameLast = "Gabriel"
                },
                new PersonEntity
                {
                    NameFirst = "Charlie",
                    NameLast = "Apple"
                },
                new PersonEntity
                {
                    NameFirst = "Jamie",
                    NameLast = "Beatriz"
                },

            };

            var compareToAsc = list.OrderBy(p => p.NameLast).ToList();
            var actualAsc = list.AsQueryable().OrderByPropertyName(nameof(PersonEntity.NameLast), true).ToList();

            actualAsc.Should().BeEquivalentTo(compareToAsc, options => options.WithStrictOrdering());

            var compareToDesc = list.OrderByDescending(p => p.NameLast).ToList();
            var actualDesc = list.AsQueryable().OrderByPropertyName(nameof(PersonEntity.NameLast), false).ToList();
            actualDesc.Should().BeEquivalentTo(compareToDesc, options => options.WithStrictOrdering());
        }
    }
}

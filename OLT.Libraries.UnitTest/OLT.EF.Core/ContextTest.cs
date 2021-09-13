using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core
{
    public class ContextTest : BaseTest
    {
        private readonly IOltIdentity _identity;
        private readonly SqlDatabaseContext _context;

        public ContextTest(
            IOltIdentity identity,
            SqlDatabaseContext context,
            ITestOutputHelper output) : base(output)
        {
            _identity = identity;
            _context = context;
        }



        [Fact]
        public void Get()
        {
            var auditUser = _context.AuditUser;
            Assert.True(_identity.GetDbUsername().Equals(auditUser));
        }


        [Fact]
        public void GetTableName()
        {
            const string expected = "People";
            var tableName = _context.GetTableName<PersonEntity>();
            Assert.Equal(tableName, expected);
        }

        [Fact]
        public void GetColumns()
        {
            const string expected = "PeopleId";
            var columns = _context.GetColumns<PersonEntity>();
            Assert.Collection(columns,
                item => Assert.Equal(item.Name, expected),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.CreateDate)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.CreateUser)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.DeletedBy)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.DeletedOn)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.ModifyDate)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.ModifyUser)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameFirst)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameLast)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameMiddle)}")
            );
        }


        // https://www.meziantou.net/testing-ef-core-in-memory-using-sqlite.htm
        //[Fact]
        //public void SqlLite()
        //{
        //    using (var factory = new SqlLiteDatabaseContextFactory())
        //    {
        //        // Get a context
        //        using (var context = factory.CreateContext())
        //        {
        //            context.People.Add(new PersonEntity
        //            {
        //                NameFirst = "Tim",
        //                NameLast = "Jones"
        //            });
        //            context.SaveChanges();
        //        }
        //    }

        //    Assert.True(true);
            
        //}

        [Fact]
        public void InitializeQueryable()
        {
            UnitTestHelper.AddPerson(_context);
            Assert.True(_context.InitializeQueryable<PersonEntity>().Any());
        }

        [Fact]
        public void GetQueryable()
        {
            UnitTestHelper.AddPerson(_context);
            _context.SaveChanges();
            Assert.True(_context.GetQueryable(new OltSearcherGetAll<PersonEntity>()).Any());
        }

        [Fact]
        public async Task SaveChangesTestAsync()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameMiddle = Faker.Name.Middle(),
                NameLast = Faker.Name.Last()
            };
            await _context.People.AddAsync(entity);
            await _context.SaveChangesAsync();

            entity.NameFirst = Faker.Name.First();
            entity.NameMiddle = string.Empty;
            await _context.SaveChangesAsync();

            var updated = await _context.GetQueryable(new OltSearcherGetAll<PersonEntity>()).FirstOrDefaultAsync(p => p.Id == entity.Id);
            Assert.True(updated.NameFirst.Equals(entity.NameFirst) && updated.NameMiddle == null);
        }

        [Fact]
        public void SaveChangesTest()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameMiddle = Faker.Name.Middle(),
                NameLast = Faker.Name.Last()
            };
            _context.People.Add(entity);
            _context.SaveChanges();

            entity.NameFirst = Faker.Name.First();
            entity.NameMiddle = string.Empty;
            _context.SaveChanges();

            var updated = _context.GetQueryable(new OltSearcherGetAll<PersonEntity>()).FirstOrDefault(p => p.Id == entity.Id);
            Assert.True(updated.NameFirst.Equals(entity.NameFirst) && updated.NameMiddle == null);
        }


        [Fact]
        public async Task ExceedMaxLength()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Lorem.Sentence(500),
                NameLast = Faker.Name.Last()
            };
            await _context.People.AddAsync(entity);
            await Assert.ThrowsAsync<DbUpdateException>(() => _context.SaveChangesAsync());

        }
    }
}
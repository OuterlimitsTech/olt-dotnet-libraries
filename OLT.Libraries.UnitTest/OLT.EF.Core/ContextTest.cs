using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using OLT.Libraries.UnitTest.Assets.Models;
using OLT.Libraries.UnitTest.Assets.Searchers;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.OLT.EF.Core
{
    public class ContextTest : BaseTest
    {
        private readonly IOltIdentity _identity;
        private readonly SqlDatabaseContext _context;
        private readonly SqlDatabaseContext2 _context2;
        private readonly IContextService _contextService;

        public ContextTest(
            IContextService contextService,
            IOltIdentity identity,
            SqlDatabaseContext context,
            SqlDatabaseContext2 context2,
            ITestOutputHelper output) : base(output)
        {
            _contextService = contextService;
            _identity = identity;
            _context = context;
            _context2 = context2;
        }

        [Fact]
        public void Get()
        {
            var auditUser = _context.AuditUser;
            Assert.True(_identity.GetDbUsername().Equals(auditUser));
        }


        [Fact]
        public void GetTableName_PersonEntity()
        {
            var expected =  $"{_context.DefaultSchema}.People";
            var tableName = _context.GetTableName<PersonEntity>();
            Assert.Equal(tableName, expected);
        }

        [Fact]
        public void GetTableName_UserEntity()
        {
            var expected = $"{_context.DefaultSchema}.Users";
            var tableName = _context.GetTableName<UserEntity>();
            Assert.Equal(tableName, expected);
        }

        [Fact]
        public void GetTableName_CodeTableType()
        {
            var expected = $"Code.{nameof(CodeTableType)}";
            var tableName = _context.GetTableName<CodeTableType>();
            Assert.Equal(tableName, expected);
        }

        [Fact]
        public void GetTableName_PersonEntity_NoSchema()
        {
            var expected = "People";
            var tableName = _context2.GetTableName<PersonEntity>();
            Assert.Equal(tableName, expected);
        }

        [Fact]
        public void GetColumns()
        {
            const string expected = "PeopleId";
            var columns = _context.GetColumns<PersonEntity>().ToList();
            columns.ForEach(col =>
            {
                Logger.Debug(col.Name);
            });
            Assert.Collection(columns,
                item => Assert.Equal(item.Name, expected),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.ActionCode)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.CreateDate)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.CreateUser)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.DeletedBy)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.DeletedOn)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.GenderId)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.ModifyDate)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.ModifyUser)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameFirst)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameLast)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.NameMiddle)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.PersonTypeId)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.SexId)}"),
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.StatusTypeId)}"), 
                item => Assert.Equal(item.Name, $"{nameof(PersonEntity.UniqueId)}")
            );
        }

        [Fact]
        public void GetColumns_CodeTable()
        {
            const string expected = "CodeTableId";
            const string discriminator = "Discriminator";
            var columns = _context.GetColumns<Gender>().ToList();
            columns.ForEach(col =>
            {
                Logger.Debug(col.Name);
            });
            Assert.Collection(columns,
                item => Assert.Equal(item.Name, expected),
                item => Assert.Equal(item.Name, $"{nameof(Gender.Code)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.CodeTableTypeId)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.CreateDate)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.CreateUser)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.DeletedBy)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.DeletedOn)}"),
                item => Assert.Equal(item.Name, discriminator),
                item => Assert.Equal(item.Name, $"{nameof(Gender.ModifyDate)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.ModifyUser)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.Name)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.ParentCodeId)}"),
                item => Assert.Equal(item.Name, $"{nameof(Gender.SortOrder)}")
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
        public async Task SaveChangesTestAsync()
        {
            //Found an issue with the null string process and we need to mix multiple entity saves within the same test
            SeedBogus(_context, 50, 75);
            SeedUsers(_context, 50, 75);

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

            var updated = await _contextService.GetAsync(new OltSearcherGetById<PersonEntity>(entity.Id));
            Assert.True(updated.NameFirst.Equals(entity.NameFirst) && updated.NameMiddle == null);
        }

        [Fact]
        public void SaveChangesTest()
        {
            //Found an issue with the null string process and we need to mix multiple entity saves within the same test
            SeedBogus(_context, 50, 75);
            SeedUsers(_context, 50, 75);
            
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

            var updated = _contextService.Get(new OltSearcherGetById<PersonEntity>(entity.Id));
            Assert.True(updated.NameFirst.Equals(entity.NameFirst) && updated.NameMiddle == null);
        }


        [Fact]
        public async Task ExceedMaxLengthAsync()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Lorem.Sentence(500),
                NameLast = Faker.Name.Last()
            };
            await _context.People.AddAsync(entity);
            await Assert.ThrowsAsync<DbUpdateException>(() => _context.SaveChangesAsync());

        }

        [Fact]
        public void ExceedMaxLength()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Lorem.Sentence(500),
                NameLast = Faker.Name.Last()
            };
            _context.People.Add(entity);
            Assert.Throws<DbUpdateException>(() => _context.SaveChanges());
        }

        [Fact]
        public async Task RegularExceptionAsync()
        {
            SeedBogus(_context, 5, 20);
            var entity = await _context.BogusNoString.OrderBy(p => Guid.NewGuid()).FirstOrDefaultAsync();
            entity.Value2 = Faker.RandomNumber.Next(1, 20000);
            await Assert.ThrowsAsync<Exception>(() => _context.SaveChangesAsync());
        }

        [Fact]
        public void RegularException()
        {
            SeedBogus(_context, 5, 20);
            var entity = _context.BogusNoString.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
            entity.Value2 = Faker.RandomNumber.Next(1, 20000);
            Assert.Throws<Exception>(() => _context.SaveChanges());
        }

        [Fact]
        public async Task FkExceptionAsync()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameLast = Faker.Name.Last(),
                PersonTypeId = Faker.RandomNumber.Next(10000)
            };
            await _context.People.AddAsync(entity);
            await Assert.ThrowsAsync<DbUpdateException>(() => _context.SaveChangesAsync());
        }

        [Fact]
        public void FkException()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameLast = Faker.Name.Last(),
                PersonTypeId = Faker.RandomNumber.Next(10000)
            };
            _context.People.Add(entity);
            Assert.Throws<DbUpdateException>(() => _context.SaveChanges());
        }

        [Fact]
        public void Delete()
        {
            var entity = new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameLast = Faker.Name.Last()
            };
            _context.People.Add(entity);
            _context.SaveChanges();

            _context.People.Remove(entity);

            Assert.True(_context.SaveChanges() > 0);
        }



        [Fact]
        public void SortOrderSet()
        {
            const int defaultSort = 9999;
            var entity = new CountryCodeEntity
            {
                UniqueId = Guid.NewGuid(),
                Code = Faker.Country.TwoLetterCode(),
                Abbreviation = Faker.Country.TwoLetterCode(),
                Name = Faker.Country.Name(),
                Description = Faker.Lorem.Sentence(10)
            };
            _context.Countries.Add(entity);
            _context.SaveChanges();
            Assert.Equal(entity.SortOrder, defaultSort);
        }


        [Fact]
        public void UniqueIdSet()
        {
            
            var entity = new CountryCodeEntity
            {
                Code = Faker.Country.TwoLetterCode(),
                Abbreviation = Faker.Country.TwoLetterCode(),
                Name = Faker.Country.Name(),
                Description = Faker.Lorem.Sentence(10),
                SortOrder = 500
            };
            _context.Countries.Add(entity);
            _context.SaveChanges();
            Assert.NotEqual(entity.UniqueId, Guid.Empty);
        }

        [Fact]
        public void CreateDateSet()
        {

            var entity = new CountryCodeEntity
            {
                UniqueId = Guid.NewGuid(),
                Code = Faker.Country.TwoLetterCode(),
                Abbreviation = Faker.Country.TwoLetterCode(),
                Name = Faker.Country.Name(),
                Description = Faker.Lorem.Sentence(10),
                SortOrder = 500,
                CreateDate = DateTimeOffset.MinValue,
            };
            _context.Countries.Add(entity);
            _context.SaveChanges();
            Assert.NotEqual(entity.UniqueId, Guid.Empty);
        }


        [Fact]
        public void AnonymousUser()
        {

            var webBuilder = new WebHostBuilder();
            webBuilder
                .UseSerilog()
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddUserSecrets<Startup>()
                        .AddJsonFile("appsettings.json", false, true)
                        .AddEnvironmentVariables();
                })
                .UseStartup<SerilogStartup>();

            var testServer = new TestServer(webBuilder);

            var context = testServer.Services.GetRequiredService<SqlDatabaseContext>();
            var entity = UnitTestHelper.AddPerson(context);
            context.SaveChanges();
            var person = context.People.FirstOrDefault(p => p.Id == entity.Id);
            Assert.Equal(person?.CreateUser, context.DefaultAnonymousUser);
        }

      
    }
}
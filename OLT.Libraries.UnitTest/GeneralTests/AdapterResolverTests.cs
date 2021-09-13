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
    public class AdapterResolverTests : BaseTest
    {
        private readonly IOltAdapterResolver _adapterResolver;
        private readonly SqlDatabaseContext _context;

        public AdapterResolverTests(
            SqlDatabaseContext context,
            IOltAdapterResolver adapterResolver,
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
            var list = new List<PersonDto>();
            for (int i = 0; i < Faker.RandomNumber.Next(10, 35); i++)
            {
                list.Add(UnitTestHelper.CreatePersonDto());
            }
            var mapped = _adapterResolver.Map<PersonDto, PersonEntity>(list).OrderBy(p => p.NameFirst).ThenBy(p => p.NameLast);
            mapped.Should().Equal(list.OrderBy(p => p.First).ThenBy(p => p.Last), (c1, c2) => c1.NameFirst == c2.First && c1.NameLast == c2.Last);
        }


        [Fact]
        public void EnumerableReverse()
        {
            var list = new List<PersonEntity>();
            for (int i = 0; i < Faker.RandomNumber.Next(10, 35); i++)
            {
                list.Add(PersonEntity.FakerEntity());
            }
            var mapped = _adapterResolver.Map<PersonEntity, PersonDto>(list).OrderBy(p => p.First).ThenBy(p => p.Last);
            mapped.Should().Equal(list.OrderBy(p => p.NameFirst).ThenBy(p => p.NameLast), (c1, c2) => c1.First == c2.NameFirst && c1.Last == c2.NameLast);
        }

        [Fact]
        public void CanProjectTo()
        {
            Assert.True(_adapterResolver.CanProjectTo<PersonEntity, PersonDto>());
        }

        [Fact]
        public void CantProjectTo()
        {
           Assert.False(_adapterResolver.CanProjectTo<AddressEntity, PersonDto>());
        }



        [Fact]
        public void QueryablePagedAdapter()
        {
            var list = new List<UserModel>
            {
                UnitTestHelper.CreateUserModel(), 
                UnitTestHelper.CreateUserModel()
            };
            var userUids = list.Select(s => s.UserGuid).ToList();
            var mapped = _adapterResolver.Map<UserModel, UserEntity>(list);
            _context.Users.AddRange(mapped);
            _context.SaveChanges();
            
            var queryable = _context.Users.Where(new OltSearcherGetAll<UserEntity>()).Where(p => userUids.Contains(p.UniqueId));
            var compareTo = _adapterResolver.Map<UserEntity, UserModel>(queryable).OrderBy(p => p.Name.First).ThenBy(p => p.Name.Last).ToList();

            compareTo.Should().Equal(list.OrderBy(p => p.Name.First).ThenBy(p => p.Name.Last), (c1, c2) => c1.Name.First == c2.Name.First && c1.Name.Last == c2.Name.Last);
        }


        [Fact]
        public void QueryableAdapter()
        {
            var list = new List<UserModel>
            {
                UnitTestHelper.CreateUserModel(),
                UnitTestHelper.CreateUserModel()
            };
            var userUids = list.Select(s => s.UserGuid).ToList();
            var mapped = _adapterResolver.Map<UserModel, UserEntity>(list);
            _context.Users.AddRange(mapped);
            _context.SaveChanges();

            var queryable = _context.Users.Where(new OltSearcherGetAll<UserEntity>()).Where(p => userUids.Contains(p.UniqueId));
            var compareTo = _adapterResolver.Map<UserEntity, UserTempDto>(queryable).OrderBy(p => p.Name.First).ThenBy(p => p.Name.Last).ToList();

            compareTo.Should().Equal(list.OrderBy(p => p.Name.First).ThenBy(p => p.Name.Last), (c1, c2) => c1.Name.First == c2.Name.First && c1.Name.Last == c2.Name.Last);
        }

        [Fact]
        public void PagedAdapter()
        {
            var queryable = _context.People.Where(new OltSearcherGetAll<PersonEntity>());
            var paged = _adapterResolver.Paged<PersonEntity, PersonDto>(queryable, new OltPagingParams { Page = 1, Size = 25 });
            Assert.Equal(paged.Data.Count(), paged.Size);
        }

        [Fact]
        public void PagedAdapterOrderBy()
        {
            var queryable = _context.People.Where(new OltSearcherGetAll<PersonEntity>());
            var paged = _adapterResolver.Paged<PersonEntity, PersonDto>(queryable, new OltPagingParams { Page = 1, Size = 25 }, query => query.OrderBy(p => p.NameFirst).ThenBy(p => p.NameLast).ThenBy(p => p.Id));
            Assert.Equal(paged.Data.Count(), paged.Size);
        }

        [Fact]
        public void DefaultOrderBy()
        {
            var orderByFunc = _adapterResolver.DefaultOrderBy<PersonEntity, PersonDto>();
            var queryable = _context.People.Where(new OltSearcherGetAll<PersonEntity>());
            var paged = orderByFunc(queryable).ToPaged(new OltPagingParams { Page = 1, Size = 25 });
            Assert.Equal(paged.Data.Count(), paged.Size);
        }

        [Fact]
        public void SortParams()
        {
            var queryable = _context.People.Where(new OltSearcherGetAll<PersonEntity>());
            var pagingParams = new OltPagingWithSortParams
            {
                Page = 1, 
                Size = 10, 
                IsAscending = false, 
                PropertyName = nameof(PersonEntity.NameLast)
            };

            var paged = _adapterResolver.Paged<PersonEntity, PersonDto>(queryable, pagingParams);
            Assert.Equal(paged.Data.Count(), paged.Size);
        }

        [Fact]
        public void Include()
        {
            var entity = UnitTestHelper.AddPersonWithAddress(_context);
            _context.SaveChanges();
            var person = _adapterResolver.Include<PersonEntity, PersonDto>(_context.People).FirstOrDefault(p => p.Id == entity.Id);
            Assert.True(person?.PersonType?.Id > 0);
        }


        [Fact]
        public void IncludeFail()
        {
            var entity = UnitTestHelper.AddPersonWithAddress(_context);
            _context.SaveChanges();
            var person = _context.People.FirstOrDefault(p => p.Id == entity.Id);
            Assert.True(person?.PersonType == null);
        }

        [Fact]
        public void NoIncludeConfigured()
        {
            var entity = UnitTestHelper.AddUser(_context);
            _context.SaveChanges();
            var user = _adapterResolver.Include<UserEntity, UserModel>(_context.Users).FirstOrDefault(p => p.Id == entity.Id);
            Assert.True(user?.Status == null);
        }

        [Fact]
        public void NoIncludeNoAdapter()
        {
            var entity = UnitTestHelper.AddUser(_context);
            _context.SaveChanges();
            var user = _adapterResolver.Include<UserEntity, PersonDto>(_context.Users).FirstOrDefault(p => p.Id == entity.Id);
            Assert.True(user?.Status == null);
        }

        [Fact]
        public void PagedAdapterNotFoundException()
        {
            var queryable = _context.Users.Where(new OltSearcherGetAll<UserEntity>());
            Assert.Throws<OltAdapterNotFoundException>(() => _adapterResolver.Paged<UserEntity, UserTempDto>(queryable, new OltPagingParams { Page = 1, Size = 10 }));
        }


        [Fact]
        public void ProjectNotFoundException()
        {
            var queryable = _context.Users.Where(new OltSearcherGetAll<UserEntity>());
            Assert.Throws<OltAdapterNotFoundException>(() => _adapterResolver.ProjectTo<UserEntity, UserDto>(queryable));
        }

        [Fact]
        public void DefaultOrderByException()
        {
            Assert.Throws<OltAdapterNotFoundException>(() => _adapterResolver.DefaultOrderBy<UserEntity, UserDto>());
        }

        [Fact]
        public void EnumerableMapNotFoundException()
        {
            var data = _context.Users.Where(new OltSearcherGetAll<UserEntity>()).Take(15).ToList();
            Assert.Throws<OltAdapterNotFoundException>(() => _adapterResolver.Map<UserEntity, UserNoAdapterDto>(data));
        }


        [Fact]
        public void QueryableMapNotFoundException()
        {
            var data = _context.Users.Where(new OltSearcherGetAll<UserEntity>()).Take(15);
            Assert.Throws<OltAdapterNotFoundException>(() => _adapterResolver.Map<UserEntity, UserNoAdapterDto>(data));
        }


        [Fact]
        public void PagedMapNotFoundException()
        {
            var queryable = _context.Users.Where(new OltSearcherGetAll<UserEntity>());
            Assert.Throws<OltAdapterNotFoundException>(() => _adapterResolver.Paged<UserEntity, UserNoAdapterDto>(queryable, new OltPagingParams { Page = 1, Size = 10}));
        }

        [Fact]
        public void PagedMapNotFoundException2()
        {
            var queryable = _context.People.Where(new OltSearcherGetAll<PersonEntity>());
            Assert.Throws<OltAdapterNotFoundException>(() => _adapterResolver.Paged<PersonEntity, PersonAutoMapperModel>(queryable, new OltPagingParams { Page = 1, Size = 10 }));
        }
    }
}
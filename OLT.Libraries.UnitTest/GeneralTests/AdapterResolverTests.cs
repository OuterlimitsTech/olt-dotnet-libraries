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
                list.Add(UnitTestHelper.CreatePerson());
            }
            var mapped = _adapterResolver.Map<PersonEntity, PersonDto>(list).OrderBy(p => p.First).ThenBy(p => p.Last);
            mapped.Should().Equal(list.OrderBy(p => p.NameFirst).ThenBy(p => p.NameLast), (c1, c2) => c1.First == c2.NameFirst && c1.Last == c2.NameLast);
        }

        [Fact]
        public void EnumerableAutoMapper()
        {
            var list = new List<PersonAutoMapperModel>();
            for (int i = 0; i < Faker.RandomNumber.Next(10,35); i++)
            {
                list.Add(UnitTestHelper.CreateTestAutoMapperModel());
            }
            var mapped = _adapterResolver.Map<PersonAutoMapperModel, PersonEntity>(list);
            mapped.Should().Equal(list, (c1, c2) => c1.NameFirst == c2.Name.First && c1.NameLast == c2.Name.Last);
        }

        [Fact]
        public void EnumerableAutoMapperReverse()
        {
            var list = new List<PersonEntity>();
            for (int i = 0; i < Faker.RandomNumber.Next(10, 35); i++)
            {
                list.Add(UnitTestHelper.CreatePerson());
            }
            var mapped = _adapterResolver.Map<PersonEntity, PersonAutoMapperModel>(list).OrderBy(p => p.Name.First).ThenBy(p => p.Name.Last);
            mapped.Should().Equal(list.OrderBy(p => p.NameFirst).ThenBy(p => p.NameLast), (c1, c2) => c1.Name.First == c2.NameFirst && c1.Name.Last == c2.NameLast);
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
        public void AdapterNotFoundException()
        {
            var item = UnitTestHelper.CreateTestAutoMapperModel();
            var address = new PersonAddressModel
            {
                Name = item.Name,
                Street1 = Faker.Address.StreetAddress()
            };

            Assert.Throws<OltAdapterNotFoundException>(() => _adapterResolver.Map(address, new UserEntity()));
        }

        //[Fact]
        //public void AdapterNotFoundException()
        //{
        //    var item = UnitTestHelper.CreateTestAutoMapperModel();
        //    var address = new PersonAddressModel
        //    {
        //        Name = item.Name,
        //        Street1 = Faker.Address.StreetAddress()
        //    };

        //    Assert.Throws<OltException>(() => _adapterResolver.Map(address, new UserEntity()));
        //}


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
            var compareTo = _adapterResolver.Map<UserEntity, UserModel>(queryable).OrderBy(p => p.Name.First).ThenBy(p => p.Name.Last).ToList();

            compareTo.Should().Equal(list.OrderBy(p => p.Name.First).ThenBy(p => p.Name.Last), (c1, c2) => c1.Name.First == c2.Name.First && c1.Name.Last == c2.Name.Last);
        }
    }
}
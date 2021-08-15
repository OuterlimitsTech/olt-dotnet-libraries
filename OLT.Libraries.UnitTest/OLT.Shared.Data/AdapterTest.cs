using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Shared.Data
{
    
    // ReSharper disable once InconsistentNaming
    public class AdapterTest : OltDisposable
    {
        private readonly IOltAdapterResolver _adapterResolver;

        public AdapterTest(IOltAdapterResolver adapterResolver)
        {
            _adapterResolver = adapterResolver;
        }

        [Fact]
        public void AutoMapperMap()
        {
            var to = new NameAutoMapperModel();
            var from = new PersonEntity();
            from.NameFirst = Faker.Name.First();
            from.NameMiddle = Faker.Name.Middle();
            from.NameLast = Faker.Name.Last();
            _adapterResolver.Map(from, to);
            Assert.True(from.NameFirst.Equals(to.First));
        }


        [Fact]
        public void AutoMapperReverse()
        {
             var from = new NameAutoMapperModel();
            var to = new PersonEntity();
            from.First = Faker.Name.First();
            from.Middle = Faker.Name.Middle();
            from.Last = Faker.Name.Last();
            _adapterResolver.Map(from, to);
            Assert.True(from.Last.Equals(to.NameLast));
        }


        [Fact]
        public void AdapterMap()
        {
            var to = new UserModel();
            var from = new UserEntity();
            from.Id = 1000;
            from.FirstName = Faker.Name.First();
            from.MiddleName = Faker.Name.Middle();
            from.LastName = Faker.Name.Last();
            _adapterResolver.Map(from, to);
            Assert.True(from.FirstName.Equals(to.Name.First));
        }


        [Fact]
        public void AdapterMapReverse()
        {
            var from = new UserModel();
            var to = new UserEntity();
            from.Name.First = Faker.Name.First();
            from.Name.Middle = Faker.Name.Middle();
            from.Name.Last = Faker.Name.Last();
            _adapterResolver.Map(from, to);
            Assert.True(from.Name.Last.Equals(to.LastName));
        }
    }
}

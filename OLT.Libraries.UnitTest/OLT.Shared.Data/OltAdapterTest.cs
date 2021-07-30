using OLT.Core;
using OLT.Libraries.UnitTest.Models;
using OLT.Libraries.UnitTest.Models.Entity;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Shared.Data
{
    
    public class OltAdapterTest
    {
        private readonly IOltAdapterResolver _adapterResolver;

        public OltAdapterTest(IOltAdapterResolver adapterResolver)
        {
            _adapterResolver = adapterResolver;
        }

        [Fact]
        public void AutoMapperMap()
        {
            var to = new OltNameTestModel();
            var from = new OltPersonEntity();
            from.NameFirst = Faker.Name.First();
            from.NameMiddle = Faker.Name.Middle();
            from.NameLast = Faker.Name.Last();
            _adapterResolver.Map(from, to);
            Assert.True(from.NameFirst.Equals(to.First));
        }


        [Fact]
        public void AutoMapperReverse()
        {
             var from = new OltNameTestModel();
            var to = new OltPersonEntity();
            from.First = Faker.Name.First();
            from.Middle = Faker.Name.Middle();
            from.Last = Faker.Name.Last();
            _adapterResolver.Map(from, to);
            Assert.True(from.Last.Equals(to.NameLast));
        }


        [Fact]
        public void AdapterMap()
        {
            var to = new OltUserTestModel();
            var from = new OltUserEntity();
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
            var from = new OltUserTestModel();
            var to = new OltUserEntity();
            from.Name.First = Faker.Name.First();
            from.Name.Middle = Faker.Name.Middle();
            from.Name.Last = Faker.Name.Last();
            _adapterResolver.Map(from, to);
            Assert.True(from.Name.Last.Equals(to.LastName));
        }
    }
}

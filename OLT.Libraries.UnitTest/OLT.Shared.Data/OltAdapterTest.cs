using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OLT.Libraries.UnitTest.OLT.Shared.Data.Adapters;
using OLT.Libraries.UnitTest.Test.Common;
using OLT.Core;
using OLT.Libraries.UnitTest.Models;
using OLT.Libraries.UnitTest.Models.Entity;

namespace OLT.Libraries.UnitTest.OLT.Shared.Data
{
    [TestClass]
    public class OltAdapterTest
    {


        [TestMethod]
        public void AutoMapperMap()
        {
            var mapper = Initialize.Provider.GetService<IMapper>();
            var adapterResolver = Initialize.Provider.GetService<IOltAdapterResolver>();
            if (adapterResolver == null)
            {
                Assert.Fail($"Unable to locate {nameof(IOltAdapterResolver)}");
            }
            var to = new OltNameTestModel();

            var from = new OltPersonEntity();
            from.NameFirst = Faker.Name.First();
            from.NameMiddle = Faker.Name.Middle();
            from.NameLast = Faker.Name.Last();
            adapterResolver.Map(from, to);
            Assert.AreEqual(from.NameFirst, to.First);
        }


        [TestMethod]
        public void AutoMapperReverse()
        {
            var adapterResolver = Initialize.Provider.GetService<IOltAdapterResolver>();
            if (adapterResolver == null)
            {
                Assert.Fail($"Unable to locate {nameof(IOltAdapterResolver)}");
            }
            var from = new OltNameTestModel();
            var to = new OltPersonEntity();
            from.First = Faker.Name.First();
            from.Middle = Faker.Name.Middle();
            from.Last = Faker.Name.Last();
            adapterResolver.Map(from, to);
            Assert.AreEqual(from.Last, to.NameLast);
        }


        [TestMethod]
        public void AdapterMap()
        {
            var adapterResolver = Initialize.Provider.GetService<IOltAdapterResolver>();
            if (adapterResolver == null)
            {
                Assert.Fail($"Unable to locate {nameof(IOltAdapterResolver)}");
            }
            var to = new OltUserTestModel();
            var from = new OltUserEntity();
            from.Id = 1000;
            from.FirstName = Faker.Name.First();
            from.MiddleName = Faker.Name.Middle();
            from.LastName = Faker.Name.Last();
            adapterResolver.Map(from, to);
            Assert.AreEqual(from.FirstName, to.Name.First);
        }


        [TestMethod]
        public void AdapterMapReverse()
        {
            var adapterResolver = Initialize.Provider.GetService<IOltAdapterResolver>();
            if (adapterResolver == null)
            {
                Assert.Fail($"Unable to locate {nameof(IOltAdapterResolver)}");
            }
            var from = new OltUserTestModel();
            var to = new OltUserEntity();
            from.Name.First = Faker.Name.First();
            from.Name.Middle = Faker.Name.Middle();
            from.Name.Last = Faker.Name.Last();
            adapterResolver.Map(from, to);
            Assert.AreEqual(from.Name.Last, to.LastName);
        }
    }
}

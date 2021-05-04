using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OLT.Libraries.UnitTest.OLT.Shared.Data.Adapters;
using OLT.Libraries.UnitTest.Test.Common;
using OLT.Core;

namespace OLT.Libraries.UnitTest.OLT.Shared.Data
{
    [TestClass]
    public class AdapterTest
    {
        [TestMethod]
        public void TestAdapterResolver()
        {
            var adapterResolver = Initialize.Provider.GetService<IOltAdapterResolver>();
            var from = new TestAdapterFromModel();
            var to = new TestAdapterToModel();
            from.First = Faker.Name.First();
            var adapter = adapterResolver.GetAdapter<TestAdapterFromModel, TestAdapterToModel>();
            adapter.Map(from, to);
            Assert.AreEqual(from.First, to.NameFirst);
        }

    }
}

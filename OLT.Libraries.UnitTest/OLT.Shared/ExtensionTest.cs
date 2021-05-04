using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OLT.Libraries.UnitTest.Test.Common;
using OLT.Core;


namespace OLT.Libraries.UnitTest.OLT.Shared
{
    public enum TestEnumTypes
    {
        [Code("test-1", 1000)]
        [System.ComponentModel.Description("Test 1")]
        Test1 = 1000,

        //[Code("test-2", 1000)]
        //[System.ComponentModel.Description("Test 2")]
        //Test2 = 2000,

    }

    [TestClass]
    public class ExtensionTests
    {
        
        [TestMethod]
        public void GetCodeEnum()
        {
            //using (var provider = Initialize.Services.BuildServiceProvider())
            //{
            //    var log = provider.GetService<IOltLogService>();
            //}

            var log2 = Initialize.Provider.GetServices<IOltLogService>();
            var code = TestEnumTypes.Test1.GetCodeEnum();
            Assert.AreEqual(code, "test-1", $"{code} do not equal");
        }

        [TestMethod]
        public void GetDescription()
        {
            var code = TestEnumTypes.Test1.GetDescription();
            Assert.AreEqual(code, "Test 1", $"{code} do not equal");
        }

        [TestMethod]
        public void GetAttributeInstance()
        {
            var attribute = OltAttributeExtensions.GetAttributeInstance<CodeAttribute>(TestEnumTypes.Test1);
            Assert.AreEqual(attribute?.Code, "test-1");
        }


        [TestMethod]
        public void GetAttributeInstance2()
        {
            var attribute = OltAttributeExtensions.GetAttributeInstance<CodeAttribute, TestEnumTypes>(TestEnumTypes.Test1);
            Assert.AreEqual(attribute?.Code, "test-1");
        }

    
    }
}

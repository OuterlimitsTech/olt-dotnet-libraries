using System.ComponentModel;
using OLT.Core;
using Xunit;


namespace OLT.Libraries.UnitTest.OLT.Shared
{
    // ReSharper disable once InconsistentNaming
    public enum TestEnumTypes
    {
        [Code("test-1", 1000)]
        [Description("Test 1")]
        Test1 = 1000,
    }


    // ReSharper disable once InconsistentNaming
    public class ExtensionTests : OltDisposable
    {

        [Fact]
        public void GetCodeEnum()
        {
            var code = TestEnumTypes.Test1.GetCodeEnum();
            Assert.True(code.Equals("test-1"), $"{code} do not equal");
        }

        [Fact]
        public void GetDescription()
        {
            var code = TestEnumTypes.Test1.GetDescription();
            Assert.True(code.Equals("Test 1"), $"{code} do not equal");
        }

        [Fact]
        public void GetAttributeInstance()
        {
            var attribute = OltAttributeExtensions.GetAttributeInstance<CodeAttribute>(TestEnumTypes.Test1);
            Assert.True(attribute?.Code.Equals("test-1"));
        }


        [Fact]
        public void GetAttributeInstance2()
        {
            var attribute = OltAttributeExtensions.GetAttributeInstance<CodeAttribute, TestEnumTypes>(TestEnumTypes.Test1);
            Assert.True(attribute?.Code.Equals("test-1"));
        }

        [Fact]
        public void FromCodeEnum()
        {
            var enumVal = "test-1".FromCodeEnum<TestEnumTypes>();
            Assert.True(enumVal == TestEnumTypes.Test1);
        }

        [Fact]
        public void FromDescription()
        {
            var enumVal = "Test 1".FromDescription<TestEnumTypes>();
            Assert.True(enumVal == TestEnumTypes.Test1);
        }
    }


    // ReSharper disable once InconsistentNaming
}

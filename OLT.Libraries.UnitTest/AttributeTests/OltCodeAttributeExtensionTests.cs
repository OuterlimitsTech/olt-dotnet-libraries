using OLT.Core;
using System;
using Xunit;

namespace OLT.Libraries.UnitTest.AttributeTests
{
    public class OltCodeAttributeExtensionTests
    {
        [Theory]
        [InlineData(null, TestAttributeEnum1.Value1)]
        [InlineData("test-value-2", TestAttributeEnum1.Value2)]
        [InlineData(null, TestAttributeEnum1.Value3)]
        [InlineData("test-value-4", TestAttributeEnum1.Value4)]
        public void GetEnumCode(string expected, TestAttributeEnum1 value)
        {
            Assert.Equal(expected, OltCodeAttributeExtensions.GetCodeEnum(value));            
        }

        [Fact]
        public void GetEnumCodeNull()
        {
            Assert.Null(OltCodeAttributeExtensions.GetCodeEnum(null));
        }

        [Theory]        
        [InlineData((short)9999, TestAttributeEnum1.Value2)]
        [InlineData(null, TestAttributeEnum1.Value3)]
        [InlineData((short)100, TestAttributeEnum1.Value4)]
        public void GetCodeEnumSort(short? expected, TestAttributeEnum1 value)
        {   
            Assert.Equal(expected, OltCodeAttributeExtensions.GetCodeEnumSort(value));
        }

        [Fact]
        public void GetCodeEnumSortNull()
        {
            Assert.Null(OltCodeAttributeExtensions.GetCodeEnumSort(null));
        }

        [Theory]
        [InlineData(TestAttributeEnum1.Value2, "test-value-2")]
        [InlineData(TestAttributeEnum1.Value4, "test-value-4")]
        [InlineData(TestAttributeEnum1.Value1, "Value1")]
        [InlineData(TestAttributeEnum1.Value2, "Value2")]
        public void FromCodeEnum(TestAttributeEnum1 expected, string value)
        {
            Assert.Equal(expected, OltCodeAttributeExtensions.FromCodeEnum<TestAttributeEnum1>(value));
        }

        [Fact]
        public void FromCodeEnumInvalid()
        {
            Assert.Throws<ArgumentException>(() => OltCodeAttributeExtensions.FromCodeEnum<TestAttributeEnum1>(Faker.Name.First()));
            Assert.Throws<ArgumentNullException>(() => OltCodeAttributeExtensions.FromCodeEnum<TestAttributeEnum1>(null));
        }

    }
}

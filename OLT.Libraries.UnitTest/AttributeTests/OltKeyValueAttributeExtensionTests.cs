////using OLT.Core;
////using Xunit;

////namespace OLT.Libraries.UnitTest.AttributeTests
////{
////    public class OltKeyValueAttributeExtensionTests
////    {
////        [Theory]
////        [InlineData(3, TestAttributeEnum1.Value3)]
////        [InlineData(0, TestAttributeEnum1.Value1)]
////        public void GetKeyValueAttributes(int expected, TestAttributeEnum1 @enum)
////        {
////            Assert.Equal(expected, OltKeyValueAttributeExtensions.GetKeyValueAttributes(@enum)?.Count);
////        }

////        [Fact]
////        public void GetKeyValueAttributesInvalid()
////        {
////            Assert.Null(OltKeyValueAttributeExtensions.GetKeyValueAttributes(null));
////        }
////    }
////}

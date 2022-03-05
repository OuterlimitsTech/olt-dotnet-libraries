////using OLT.Core;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using Xunit;

////namespace OLT.Libraries.UnitTest.AttributeTests
////{

////    public enum TestEmptyEnum
////    {

////    }

////    public enum TestAttributeEnum1
////    {
////        [Description("Test Value 1")]
////        Value1,

////        [Code("test-value-2")]
////        Value2,

////        [KeyValue("key1", "value1")]
////        [KeyValue("key2", "value2")]
////        [KeyValue("key3", "value3")]
////        Value3,

////        [Code("test-value-4", 100)]
////        Value4,

////    }

////    public enum TestAttributeEnum2
////    {
////        [CustomTest("Test Value 1")]
////        [CustomTest("Test Value 1")]
////        Value1,
////    }

////    public class TestAttributeClassBase
////    {
////        [Description("Base Class Value1")]
////        [CustomTest("Value1 - Item1")]
////        [CustomTest("Value1 - Item2")]
////        [CustomTest("Value1 - Item3")]
////        public virtual string Value1 { get; set; }

////        [Description("Base Class Value2")]
////        [CustomTest("Value2 - Item1")]
////        [CustomTest("Value2 - Item2")]
////        public virtual string Value2 { get; set; }

////        [Description("Base Class Value3")]
////        [CustomTest("Value3 - Item1")]
////        [CustomTest("Value3 - Item2")]
////        public virtual string Value3 { get; set; }

////        public virtual string Value4 { get; set; }
////    }

////    public class TestAttributeClass : TestAttributeClassBase
////    {
////        [Description("Test Class Value1")]
////        [CustomTest("Value1 - Item800")]
////        [CustomTest("Value1 - Item900")]
////        public override string Value1 { get; set; }

////        public override string Value2 { get; set; }

////        public string Value5 { get; set; }
////    }

////    public class KeyValueAttributesTests
////    {

////        [Fact]
////        public void GetAttributes()
////        {
            

////        }



////    }
////}

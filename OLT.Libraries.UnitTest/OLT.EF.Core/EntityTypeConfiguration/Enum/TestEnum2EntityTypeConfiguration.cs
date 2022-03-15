////using OLT.Core;
////using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;
////using System;
////using Xunit;

////namespace OLT.Libraries.UnitTest.OLT.EF.Core.EntityTypeConfiguration.Enum
////{
////    public enum TestEnumConfiguration
////    {
////        Value1 = 1234,
////        Value2 = 9876,
////        Value3 = -500
////    }

////    public class TestEnum2EntityTypeConfiguration : BaseTestEnumEntityTypeConfiguration<CodeTableType, TestEnumConfiguration>
////    {
////        public override void RunMapTests()
////        {
////            Assert.Throws<OltException>(() => base.Map());
////        }
////    }
////}

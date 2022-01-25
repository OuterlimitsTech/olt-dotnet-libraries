using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;
using System;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.EntityTypeConfiguration.Enum
{
    public enum TestByteEnum : byte 
    { 
        Value1, 
        Value2,
    }

    public class TestByteEntityTypeConfiguration : BaseTestEnumEntityTypeConfiguration<PersonTypeCodeEntity, TestByteEnum>
    {

        public override void RunMapTests(TestByteEnum @enum, string code, string name, short sortOrder, Guid? uidResult = null)
        {
            var entity = new PersonTypeCodeEntity();
            Assert.Throws<InvalidCastException>(() => base.Map(entity, @enum));
        }

        public override void RunMapTests()
        {
            Assert.Throws<InvalidCastException>(() => base.Map());
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.EF.Core.EntityTypeConfiguration.Enum
{
    public class EntityTypeConfigurationTests
    {
        [Fact]
        public void Test1()
        {
            var config = new TestEnum1EntityTypeConfiguration();
            var @enum = CodeTableTypes.GenderTypes;
            var code = OltAttributeExtensions.GetAttributeInstance<CodeAttribute, CodeTableTypes>(@enum)?.Code;
            var name = OltAttributeExtensions.GetAttributeInstance<DescriptionAttribute, CodeTableTypes>(@enum)?.Description;
            var sortOrder = OltAttributeExtensions.GetAttributeInstance<CodeAttribute, CodeTableTypes>(@enum)?.DefaultSort ?? -1;
            config.RunMapTests(@enum, code, name, sortOrder);

            @enum = CodeTableTypes.SexTypes;
            code = OltAttributeExtensions.GetAttributeInstance<CodeAttribute, CodeTableTypes>(@enum)?.Code;
            name = OltAttributeExtensions.GetAttributeInstance<DescriptionAttribute, CodeTableTypes>(@enum)?.Description;
            sortOrder = OltAttributeExtensions.GetAttributeInstance<CodeAttribute, CodeTableTypes>(@enum)?.DefaultSort ?? -1;
            config.RunMapTests(@enum, code, name, sortOrder);

            config.RunMapTests();
        }


        [Fact]
        public void Test2()
        {
            var config = new TestEnum2EntityTypeConfiguration();
            var @enum = TestEnumConfiguration.Value1;
            var value = System.Enum.GetName(@enum);
            short sortOrder = 9999;
            config.RunMapTests(@enum, value, value, sortOrder);

            @enum = TestEnumConfiguration.Value2;
            value = System.Enum.GetName(@enum);
            config.RunMapTests(@enum, value, value, sortOrder);

            @enum = TestEnumConfiguration.Value3;
            value = System.Enum.GetName(@enum);
            Assert.Throws<OltException>(() => config.RunMapTests(@enum, value, value, sortOrder));            

            config.RunMapTests();
        }


        [Fact]
        public void BtyeEnum()
        {
            var config = new TestByteEntityTypeConfiguration();
            var @enum = TestByteEnum.Value1;
            config.RunMapTests(@enum, null, null, -100);
            config.RunMapTests();
        }

        [Fact]
        public void Uid()
        {
            var config = new TestPersonTypeEntityTypeConfiguration();
            var @enum = PersonTypeEnumConfiguration.Type1;
            var value = System.Enum.GetName(@enum);
            short sortOrder = 9999;
            var uid = OltAttributeExtensions.GetAttributeInstance<UniqueIdAttribute, PersonTypeEnumConfiguration>(@enum)?.UniqueId;
            config.RunMapTests(@enum, value, value, sortOrder, uid);            


            @enum = PersonTypeEnumConfiguration.Type2;
            var code = System.Enum.GetName(@enum);
            var name = OltAttributeExtensions.GetAttributeInstance<DescriptionAttribute, PersonTypeEnumConfiguration>(@enum)?.Description;
            config.RunMapTests(@enum, code, name, sortOrder);

            config.RunMapTests();
        }

        
    }
}

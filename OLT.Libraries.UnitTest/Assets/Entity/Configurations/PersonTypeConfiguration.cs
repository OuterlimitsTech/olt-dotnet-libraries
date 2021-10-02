using System.Reflection;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Entity.Configurations
{
    public class PersonTypeConfiguration : OltEntityTypeConfigurationFromCsv<PersonTypeCodeEntity, PersonTypeCsvModel>
    {
        protected override string ResourceName => "person_type.csv";
        protected override Assembly ResourceAssembly => this.GetType().Assembly;
    }
}
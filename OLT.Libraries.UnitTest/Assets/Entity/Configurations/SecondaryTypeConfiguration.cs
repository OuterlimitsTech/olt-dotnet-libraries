using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Entity.Configurations
{
    public class SecondaryTypeConfiguration : OltEntityTypeConfigurationFromCsv<SecondaryTypeCodeEntity, SecondaryTypeCsvModel>
    {
        protected override string ResourceName => "person_type.csv";
        protected override Assembly ResourceAssembly => this.GetType().Assembly;

        public override void Configure(EntityTypeBuilder<SecondaryTypeCodeEntity> builder)
        {
            short idx = 5000;
            this.Seed(builder, (entity, model) =>
            {
                entity.SortOrder = idx;
                idx--;
            });
        }
    }
}
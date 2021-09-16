using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;

namespace OLT.Libraries.UnitTest.Assets.Entity.Configurations
{
    public abstract class BaseCodeTableEntityConfiguration<TEntity> : OltEntityTypeConfiguration<TEntity>
        where TEntity : CodeTable, new()
    {
        protected abstract List<TEntity> SeedData { get; }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var seeds = SeedData.ToList();

            seeds.ForEach(val =>
            {
                val.CodeTableTypeId = (int)val.CodeTableTypeEnum;
                val.CreateUser = DefaultUsername;
                val.CreateDate = DefaultCreateDate;

                if (val.SortOrder <= 0)
                {
                    val.SortOrder = 9999;
                }
            });

            builder.HasData(seeds);

        }
    }
}
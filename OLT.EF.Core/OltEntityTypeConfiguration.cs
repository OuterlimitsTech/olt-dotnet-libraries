using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OLT.Core
{
    public abstract class OltEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IOltEntity, new()
    {

        
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
        protected virtual DateTimeOffset DefaultCreateDate => new DateTimeOffset(1980, 1, 1, 0, 0, 0, 0, DateTimeOffset.UtcNow.Offset);
        protected virtual string DefaultUsername => "SystemLoad";

        protected short GetEnumCodeSortOrder<TEnum>(TEnum item)
            where TEnum : struct
        {
            return OltAttributeExtensions.GetAttributeInstance<CodeAttribute, TEnum>(item)?.DefaultSort ?? 9999;
        }

        protected string GetEnumDescription<TEnum>(TEnum item)
            where TEnum : struct
        {
            return OltAttributeExtensions.GetAttributeInstance<DescriptionAttribute, TEnum>(item)?.Description;
        }

        protected string GetEnumCode<TEnum>(TEnum item)
            where TEnum : struct
        {
            return OltAttributeExtensions.GetAttributeInstance<CodeAttribute, TEnum>(item)?.Code;
        }

        protected Guid? GetUniqueId<TEnum>(TEnum item)
            where TEnum : struct
        {
            return OltAttributeExtensions.GetAttributeInstance<UniqueIdAttribute, TEnum>(item)?.UniqueId;
        }

        protected static string GetColumnName(PropertyInfo item)
        {
            return OltAttributeExtensions.GetAttributeInstance<ColumnAttribute>(item)?.Name;
        }

    
    }
}
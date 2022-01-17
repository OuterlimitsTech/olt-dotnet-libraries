using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OLT.Core
{
    public class OltEntityTypeConfiguration<TEntity, TEnum> : OltEntityTypeConfiguration<TEntity>
        where TEntity : class, IOltEntity, IOltEntityCodeValueEnum, new()
        where TEnum : struct
    {

        protected virtual Type EnumType => typeof(TEnum);

        protected virtual void Map(TEntity entity, TEnum @enum)
        {
            entity.Code = GetEnumCode(@enum) ?? Enum.GetName(EnumType, @enum);
            entity.Name = GetEnumDescription(@enum) ?? Enum.GetName(EnumType, @enum);
            entity.SortOrder = GetEnumCodeSortOrder(@enum);

            if (entity is IOltEntityAudit auditEntity)
            {
                auditEntity.CreateUser = DefaultUsername;
                auditEntity.CreateDate = DefaultCreateDate;
            }

            if (entity is IOltEntityUniqueId uidTableEntity)
            {
                var uid = GetUniqueId(@enum);
                if (uid.HasValue)
                {
                    uidTableEntity.UniqueId = uid.Value;
                }
                else if (uidTableEntity.UniqueId == Guid.Empty)
                {
                    uidTableEntity.UniqueId = Guid.NewGuid();
                }
            }

            if (entity is IOltEntitySortable sortableEntity)
            {
                sortableEntity.SortOrder = (short)9999;
            }
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (!EnumType.IsEnum)
                throw new InvalidCastException($"Type '{EnumType.AssemblyQualifiedName}' must be enum");

            var nType = Enum.GetUnderlyingType(EnumType);

            if (nType == typeof(long) || nType == typeof(ulong) || nType == typeof(uint))
                throw new InvalidCastException($"Type '{EnumType.AssemblyQualifiedName}' must be of type long, ulong, uint");

            var list = new List<TEntity>();
            foreach (TEnum enumValue in Enum.GetValues(EnumType))
            {
                var id = (int)Convert.ChangeType(enumValue, typeof(int));

                if (id <= 0)
                {
                    throw new InvalidCastException("Enum underlying value must be positive");
                }
                var item = new TEntity { Id = id };
                Map(item, enumValue);
                list.Add(item);
            }

            if (list.Any())
            {
                builder.HasData(list);
            }
        }
    }

    [Obsolete("Move to OltEntityTypeConfiguration<TEntity, TEnum>")]
    public class OltEntityTypeConfigurationFromEnum<TEntity, TEnum> : OltEntityTypeConfiguration<TEntity, TEnum>
        where TEntity : class, IOltEntity, IOltEntityCodeValueEnum, new()
        where TEnum : struct
    {

        protected virtual void Seed(EntityTypeBuilder<TEntity> builder, Action<TEntity, TEnum> setProperties = null)
        {

            var eType = typeof(TEnum);

            if (!eType.IsEnum)
                throw new InvalidCastException($"Type '{eType.AssemblyQualifiedName}' must be enum");

            var nType = Enum.GetUnderlyingType(eType);

            if (nType == typeof(long) || nType == typeof(ulong) || nType == typeof(uint))
                throw new InvalidCastException($"Type '{eType.AssemblyQualifiedName}' must be of type long, ulong, uint");

            var list = new List<TEntity>();
            foreach (TEnum enumValue in Enum.GetValues(eType))
            {
                var id = (int)Convert.ChangeType(enumValue, typeof(int));

                if (id <= 0)
                {
                    throw new InvalidCastException("Enum underlying value must be positive");
                }
                var item = new TEntity
                {
                    Id = id
                };

                Map(item, enumValue);
                setProperties?.Invoke(item, enumValue);

                list.Add(item);
            }

            if (list.Any())
            {
                builder.HasData(list);
            }

        }

        
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.Seed(builder);
        }
    }
}
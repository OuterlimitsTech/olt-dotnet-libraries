using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        protected virtual short GetEnumCodeSortOrder<TEnum>(TEnum item, short defaultValue = 9999)
            where TEnum : System.Enum
        {
            return OltAttributeExtensions.GetAttributeInstance<CodeAttribute, TEnum>(item)?.DefaultSort ?? defaultValue;
        }

        protected virtual string GetEnumDescription<TEnum>(TEnum item)
            where TEnum : System.Enum
        {
            return OltAttributeExtensions.GetAttributeInstance<DescriptionAttribute, TEnum>(item)?.Description;
        }

        protected virtual string GetEnumCode<TEnum>(TEnum item)
            where TEnum : System.Enum
        {
            return OltAttributeExtensions.GetAttributeInstance<CodeAttribute, TEnum>(item)?.Code;
        }

        protected virtual Guid? GetUniqueId<TEnum>(TEnum item)
            where TEnum : System.Enum
        {
            return OltAttributeExtensions.GetAttributeInstance<UniqueIdAttribute, TEnum>(item)?.UniqueId;
        }

        protected virtual string GetColumnName(PropertyInfo item)
        {
            return OltAttributeExtensions.GetAttributeInstance<ColumnAttribute>(item)?.Name;
        }    
    }


    public class OltEntityTypeConfiguration<TEntity, TEnum> : OltEntityTypeConfiguration<TEntity>
        where TEntity : class, IOltEntity, IOltEntityId, new()
        where TEnum : System.Enum
    {

        protected virtual Type EnumType => typeof(TEnum);
        protected virtual Type UnderlyingType => Enum.GetUnderlyingType(EnumType);
        protected virtual int MinimumValue => 1;
        protected virtual short DefaultSort => 9999;

        protected virtual void Map(TEntity entity, TEnum @enum)
        {
            if (!(UnderlyingType == typeof(uint) || UnderlyingType == typeof(int)))
                throw new InvalidCastException($"Type '{EnumType.AssemblyQualifiedName}' must be of type uint, int");

            var id = (int)Convert.ChangeType(@enum, typeof(int));

            if (id < MinimumValue)
            {
                throw new OltException($"Enum underlying value must be greater or equal to {MinimumValue}");
            }

            entity.Id = id;

            if (entity is IOltEntityCodeValue codeValueEntity)
            {
                codeValueEntity.Code = GetEnumCode(@enum) ?? Enum.GetName(EnumType, @enum);
                codeValueEntity.Name = GetEnumDescription(@enum) ?? Enum.GetName(EnumType, @enum);
            }

            if (entity is IOltEntitySortable sortableEntity)
            {
                sortableEntity.SortOrder = GetEnumCodeSortOrder(@enum, DefaultSort);
            }

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

        }

        protected virtual List<TEntity> Map()
        {
            var list = new List<TEntity>();
            foreach (TEnum enumValue in Enum.GetValues(EnumType))
            { 
                var entity = new TEntity();
                Map(entity, enumValue);
                list.Add(entity);
            }
            return list; 
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var list = Map();
            if (list.Any())
            {
                builder.HasData(list);
            }
        }
    }
}
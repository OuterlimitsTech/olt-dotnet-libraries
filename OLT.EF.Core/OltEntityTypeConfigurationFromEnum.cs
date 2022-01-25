using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OLT.Core
{   

    [Obsolete("Move to OltEntityTypeConfiguration<TEntity, TEnum>")]
    public class OltEntityTypeConfigurationFromEnum<TEntity, TEnum> : OltEntityTypeConfiguration<TEntity, TEnum>
        where TEntity : class, IOltEntity, IOltEntityCodeValueEnum, new()
        where TEnum : System.Enum
    {

        protected virtual void Seed(EntityTypeBuilder<TEntity> builder, Action<TEntity, TEnum> setProperties = null)
        {

            var eType = typeof(TEnum);

            var nType = Enum.GetUnderlyingType(eType);

            if (nType == typeof(long) || nType == typeof(ulong) || nType == typeof(uint) || nType == typeof(int))
                throw new InvalidCastException($"Type '{eType.AssemblyQualifiedName}' must be of type long, ulong, uint, int");

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
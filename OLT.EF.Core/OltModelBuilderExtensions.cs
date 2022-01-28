using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OLT.Core
{
    public static class OltModelBuilderExtensions
    {
        public static ModelBuilder EntitiesOfType<T>(this ModelBuilder modelBuilder,
            Action<EntityTypeBuilder> buildAction) 
        {
            return modelBuilder.EntitiesOfType(typeof(T), buildAction);
        }

        public static ModelBuilder EntitiesOfType(this ModelBuilder modelBuilder, Type type,
            Action<EntityTypeBuilder> buildAction)
        {         
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(entityType => type.IsAssignableFrom(entityType.ClrType)))
            {
                buildAction(modelBuilder.Entity(entityType.ClrType));
            }

            return modelBuilder;
        }
    }
}
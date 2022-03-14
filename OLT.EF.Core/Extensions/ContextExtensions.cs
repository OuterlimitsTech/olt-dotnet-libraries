using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    public static class ContextExtensions
    {

        //!!!!! USE TO BULK COPY !!!!!!
        // EFCore.BulkExtensions


        public static string GetTableName<TEntity>(this DbContext context) where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));

            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            return string.IsNullOrEmpty(schema) ? tableName : $"{schema}.{tableName}";
        }

        public static IEnumerable<OltDbColumnInfo> GetColumns<TEntity>(this DbContext dbContext)
            where TEntity : class
        {
            var cols = new List<OltDbColumnInfo>();
            var entityType = dbContext.Model.FindEntityType(typeof(TEntity));

            // Table info 
            var tableName = entityType.GetTableName();
            var tableSchema = entityType.GetSchema();

            // Column info 
            foreach (var property in entityType.GetProperties())
            {
                var column = new OltDbColumnInfo
                {
                    Name = property.GetColumnName(StoreObjectIdentifier.Table(tableName, tableSchema)),
                };

                try
                {
                    column.Type = property.GetColumnType();
                }
                catch
                {
                    column.Type = property.GetTypeMapping().ClrType.Name;
                }
                cols.Add(column);
            }

            return cols;
        }

        public static IQueryable<TEntity> InitializeQueryable<TEntity>(this IOltDbContext context)
            where TEntity : class, IOltEntity
        {
            return InitializeQueryable<TEntity>(context, true);
        }

        public static IQueryable<TEntity> InitializeQueryable<TEntity>(this IOltDbContext context, bool includeDeleted)
            where TEntity : class, IOltEntity
        {
            var query = context.Set<TEntity>().AsQueryable();
            if (context.ApplyGlobalDeleteFilter)
            {
                if (includeDeleted)
                {
                    query = query.IgnoreQueryFilters();
                }
            }
            else if (!includeDeleted)
            {
                query = OltGeneralQueryableExtensions.NonDeletedQueryable(query);
            }
            return query;
        }

        public static void SetSoftDeleteGlobalFilter(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyGlobalFilters<IOltEntityDeletable>(p => p.DeletedOn == null);
        }


        /// <summary>
        /// <see href="https://davecallan.com/entity-framework-core-query-filters-multiple-entities"/>
        /// </summary>
        /// <example>
        /// <code>
        /// modelBuilder.HasQueryFilter(p => p.DeletedOn == null)
        /// </code>
        /// </example>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="expression"></param>
        public static void ApplyGlobalFilters<TEntity>(this ModelBuilder modelBuilder, Expression<Func<TEntity, bool>> expression)
        {
#pragma warning disable S125
            modelBuilder.EntitiesOfType<TEntity>(builder =>
            {
                var clrType = builder.Metadata.ClrType;

#if NET6_0_OR_GREATER
                //TPH class?
                if (!builder.Metadata.GetDefaultTableName().Equals(builder.Metadata.GetTableName(), StringComparison.OrdinalIgnoreCase) && builder.Metadata.GetDiscriminatorPropertyName() != null)
                {
                    //Console.WriteLine($"GetDiscriminatorProperty: {builder.Metadata.GetDiscriminatorProperty()} of type {builder.Metadata.ClrType.FullName}");
                    clrType = clrType.BaseType;
                    //Console.WriteLine($"{builder.Metadata.GetTableName()} not equal to {builder.Metadata.GetDefaultTableName()} of type {builder.Metadata.ClrType.FullName}");
                }
#else
                //TPH class?
                if (!builder.Metadata.GetDefaultTableName().Equals(builder.Metadata.GetTableName(), StringComparison.OrdinalIgnoreCase) &&
                    builder.Metadata.GetDiscriminatorProperty()?.GetColumnName(StoreObjectIdentifier.Table(builder.Metadata.GetTableName(), builder.Metadata.GetSchema())) != null)
                {

                    //Console.WriteLine($"GetDiscriminatorProperty: {builder.Metadata.GetDiscriminatorProperty()} of type {builder.Metadata.ClrType.FullName}");
                    clrType = clrType.BaseType;
                    //Console.WriteLine($"{builder.Metadata.GetTableName()} not equal to {builder.Metadata.GetDefaultTableName()} of type {builder.Metadata.ClrType.FullName}");
                }
#endif
                var newParam = Expression.Parameter(clrType);
                var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                modelBuilder.Entity(clrType).HasQueryFilter(Expression.Lambda(newBody, newParam));

            });
#pragma warning restore S125

        }        


    }
}
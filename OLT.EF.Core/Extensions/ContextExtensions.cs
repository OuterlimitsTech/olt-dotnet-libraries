using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{

    public enum OltConnectionStringTypes
    {
        [Code("database.windows.net")]
        AzureSql
    }

    public static class ContextExtensions
    {

        //!!!!! USE TO BULK COPY !!!!!!
        // EFCore.BulkExtensions


        public static string GetTableName<T>(this DbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));

            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();

            return string.IsNullOrEmpty(schema) ? $"{tableName}" : $"{schema}.{tableName}";
        }

        public static IEnumerable<OltDbColumnInfo> GetColumns<T>(this DbContext dbContext)
            where T : class
        {
            var cols = new List<OltDbColumnInfo>();
            var entityType = dbContext.Model.FindEntityType(typeof(T));

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




        public static IQueryable<T> InitializeQueryable<T>(this IOltDbContext context)
            where T : class, IOltEntity
        {
            return InitializeQueryable<T>(context, true);
        }

        public static IQueryable<T> InitializeQueryable<T>(this IOltDbContext context, bool includeDeleted)
            where T : class, IOltEntity
        {
            var query = context.Set<T>().AsQueryable();
            if (context.ApplyGlobalDeleteFilter)
            {
                if (includeDeleted)
                {
                    query = query.IgnoreQueryFilters();
                }
            }
            else if (!includeDeleted)
            {
                query = NonDeletedQueryable(context, query);
            }
            return query;
        }

        public static IQueryable<T> NonDeletedQueryable<T>(this IOltDbContext context, IQueryable<T> queryable)
            where T : class, IOltEntity
        {
            if (!typeof(IOltEntityDeletable).IsAssignableFrom(typeof(T))) return queryable;
            Expression<Func<T, bool>> getNonDeleted = deletableQuery => ((IOltEntityDeletable)deletableQuery).DeletedOn == null;
            getNonDeleted = (Expression<Func<T, bool>>)OltRemoveCastsVisitor.Visit(getNonDeleted);
            return queryable.Where(getNonDeleted);
        }


        public static IQueryable<T> GetQueryable<T>(this IOltDbContext context, IOltSearcher<T> queryBuilder)
            where T : class, IOltEntity
        {
            return queryBuilder.BuildQueryable(InitializeQueryable<T>(context, queryBuilder.IncludeDeleted));
        }

        public static IQueryable<TEntity> GetQueryable<TEntity>(this IOltDbContext context, params IOltSearcher<TEntity>[] searchers) where TEntity : class, IOltEntity
        {
            var queryable = InitializeQueryable<TEntity>(context, searchers.Any(p => p.IncludeDeleted));
            searchers.ToList().ForEach(builder =>
            {
                queryable = builder.BuildQueryable(queryable);
            });
            return queryable;
        }

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, IOltEntityDeletable
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(p => p.DeletedOn == null);
        }

        public static void SetSoftDeleteGlobalFilter(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyGlobalFilters<IOltEntityDeletable>(p => p.DeletedOn == null);
        }


        /// <summary>
        /// modelBuilder.Entity<EF_POCO>().HasQueryFilter(p => p.DeletedOn == null)
        /// https://davecallan.com/entity-framework-core-query-filters-multiple-entities/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="expression"></param>
        public static void ApplyGlobalFilters<T>(this ModelBuilder modelBuilder, Expression<Func<T, bool>> expression)
        {
#pragma warning disable S125
            modelBuilder.EntitiesOfType<T>(builder =>
            {
                var clrType = builder.Metadata.ClrType;

                //TPH class?
                if (!builder.Metadata.GetDefaultTableName().Equals(builder.Metadata.GetTableName(), StringComparison.OrdinalIgnoreCase) &&
                    builder.Metadata.GetDiscriminatorProperty()?.GetColumnName(StoreObjectIdentifier.Table(builder.Metadata.GetTableName(), builder.Metadata.GetSchema())) != null)
                {

                    //Console.WriteLine($"GetDiscriminatorProperty: {builder.Metadata.GetDiscriminatorProperty()} of type {builder.Metadata.ClrType.FullName}");
                    clrType = clrType.BaseType;
                    //Console.WriteLine($"{builder.Metadata.GetTableName()} not equal to {builder.Metadata.GetDefaultTableName()} of type {builder.Metadata.ClrType.FullName}");
                }

                var newParam = Expression.Parameter(clrType);
                var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                modelBuilder.Entity(clrType).HasQueryFilter(Expression.Lambda(newBody, newParam));
            });

#pragma warning restore S125
        }



        public static bool IsProductionDb(this DbContext context, string searchFor)
        {
            return context.Database.GetConnectionString().Contains(searchFor, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsProductionDb(this DbContext context, OltConnectionStringTypes searchFor)
        {
            return IsProductionDb(context, searchFor.GetCodeEnum());
        }
    }
}
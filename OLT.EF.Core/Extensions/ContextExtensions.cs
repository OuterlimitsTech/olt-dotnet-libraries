////using System;
////using System.Collections.Generic;
////using System.Data;
////using System.Data.Entity;
////using System.Data.Entity.Core.Mapping;
////using System.Data.Entity.Core.Metadata.Edm;
////using System.Data.Entity.Core.Objects;
////using System.Data.Entity.Infrastructure;
////using System.Data.SqlClient;
////using System.Diagnostics;
////using System.Linq;
////using System.Reflection;
////using System.Text.RegularExpressions;

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
    public static class ContextExtensions
    {

        //!!!!! USE TO BULK COPY !!!!!!
        // EFCore.BulkExtensions


        public static string GetTableName<T>(this DbContext context) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));

            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();

            return $"{schema}.{tableName}";
        }

        public static IEnumerable<DbColumnInfo> GetColumns<T>(this DbContext dbContext)
            where T : class
        {
            var cols = new List<DbColumnInfo>();
            var entityType = dbContext.Model.FindEntityType(typeof(T));

            // Table info 
            var tableName = entityType.GetTableName();
            var tableSchema = entityType.GetSchema();

            // Column info 
            foreach (var property in entityType.GetProperties())
            {
                cols.Add(new DbColumnInfo
                {
                    Name = property.GetColumnName(StoreObjectIdentifier.Table(tableName, tableSchema)),
                    Type = property.GetColumnType(),
                });
            };

            return cols;
        }

        #region [ EF FLUENT PROC ]

        /**************************************************************************************************************************************************
            START EF FLUENT PROC 
            These extensions came from https://github.com/snickler/EFCore-FluentStoredProcedure/blob/master/EFCoreFluent/src/EFCoreFluent/EFExtensions.cs 
        ***************************************************************************************************************************************************/



        /// <summary>
        /// Creates an initial DbCommand object based on a stored procedure name
        /// </summary>
        /// <param name="context">target database context</param>
        /// <param name="storedProcName">target procedure name</param>
        /// <param name="prependDefaultSchema">Prepend the default schema name to <paramref name="storedProcName"/> if explicitly defined in <paramref name="context"/></param>
        /// <param name="commandTimeout">Command timeout in seconds. Default is 30.</param>
        /// <returns></returns>
        public static DbCommand LoadStoredProc(this DbContext context, string storedProcName,
            bool prependDefaultSchema = true, short commandTimeout = 30)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandTimeout = commandTimeout;

            if (prependDefaultSchema)
            {
                var schemaName = context.Model["DefaultSchema"];
                if (schemaName != null)
                {
                    storedProcName = $"{schemaName}.{storedProcName}";
                }
            }

            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }

        /// <summary>
        /// Creates a DbParameter object and adds it to a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <param name="configureParam"></param>
        /// <returns></returns>
        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue,
            Action<DbParameter> configureParam = null)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue ?? DBNull.Value;
            configureParam?.Invoke(param);
            cmd.Parameters.Add(param);
            return cmd;
        }

        /// <summary>
        /// Creates a DbParameter object and adds it to a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paramName"></param>
        /// <param name="configureParam"></param>
        /// <returns></returns>
        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName,
            Action<DbParameter> configureParam = null)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            configureParam?.Invoke(param);
            cmd.Parameters.Add(param);
            return cmd;
        }

        /// <summary>
        /// Adds a SqlParameter to a DbCommand.
        /// This enabled the ability to provide custom types for SQL-parameters.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static DbCommand WithSqlParam(this DbCommand cmd, IDbDataParameter parameter)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            cmd.Parameters.Add(parameter);

            return cmd;
        }

        /// <summary>
        /// Adds an array of SqlParameters to a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static DbCommand WithSqlParams(this DbCommand cmd, IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(cmd.CommandText) && cmd.CommandType != System.Data.CommandType.StoredProcedure)
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            cmd.Parameters.AddRange(parameters);

            return cmd;
        }

        public class SprocResults
        {
            private readonly DbDataReader _reader;

            public SprocResults(DbDataReader reader)
            {
                _reader = reader;
            }

            public IList<T> ReadToList<T>() where T : new()
            {
                return MapToList<T>(_reader);
            }

            public T? ReadToValue<T>() where T : struct
            {
                return MapToValue<T>(_reader);
            }

            public Task<bool> NextResultAsync()
            {
                return _reader.NextResultAsync();
            }

            public Task<bool> NextResultAsync(CancellationToken ct)
            {
                return _reader.NextResultAsync(ct);
            }

            public bool NextResult()
            {
                return _reader.NextResult();
            }

            /// <summary>
            /// Retrieves the column values from the stored procedure and maps them to <typeparamref name="T"/>'s properties
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="dr"></param>
            /// <returns>IList&lt;<typeparam name="T">&gt;</typeparam></returns>
            private static IList<T> MapToList<T>(DbDataReader dr) where T : new()
            {
                var objList = new List<T>();
                var props = typeof(T).GetRuntimeProperties().ToList();

                var colMapping = dr.GetColumnSchema()
                    .Where(x => props.Any(y =>
                        string.Equals(y.Name, x.ColumnName, StringComparison.CurrentCultureIgnoreCase)))
                    .ToDictionary(key => key.ColumnName.ToUpper());

                if (!dr.HasRows)
                    return objList;

                while (dr.Read())
                {
                    var obj = new T();
                    foreach (var prop in props)
                    {
                        var upperName = prop.Name.ToUpper();

                        if (!colMapping.ContainsKey(upperName))
                            continue;

                        var column = colMapping[upperName];

                        if (column?.ColumnOrdinal == null)
                            continue;

                        var val = dr.GetValue(column.ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }

                    objList.Add(obj);
                }

                return objList;
            }

            /// <summary>
            /// Attempts to read the first value of the first row of the result set.
            /// </summary>
            private static T? MapToValue<T>(DbDataReader dr) where T : struct
            {
                if (!dr.HasRows)
                    return new T?();

                if (dr.Read())
                {
                    return dr.IsDBNull(0) ? new T?() : dr.GetFieldValue<T>(0);
                }

                return new T?();
            }
        }

        /// <summary>
        /// Executes a DbDataReader and passes the results to <paramref name="handleResults"/>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handleResults"></param>
        /// <param name="commandBehaviour"></param>
        /// <param name="manageConnection"></param>
        /// <returns></returns>
        public static void ExecuteStoredProc(this DbCommand command, Action<SprocResults> handleResults,
            CommandBehavior commandBehaviour = CommandBehavior.Default,
            bool manageConnection = true)
        {
            if (handleResults == null)
            {
                throw new ArgumentNullException(nameof(handleResults));
            }

            using (command)
            {
                if (manageConnection && command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (var reader = command.ExecuteReader(commandBehaviour))
                    {
                        var sprocResults = new SprocResults(reader);
                        handleResults(sprocResults);
                    }
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Executes a DbDataReader asynchronously and passes the results to <paramref name="handleResults"/>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handleResults"></param>
        /// <param name="commandBehaviour"></param>
        /// <param name="ct"></param>
        /// <param name="manageConnection"></param>
        /// <returns></returns>
        public static async Task ExecuteStoredProcAsync(this DbCommand command, Action<SprocResults> handleResults,
            System.Data.CommandBehavior commandBehaviour = System.Data.CommandBehavior.Default,
            CancellationToken ct = default, bool manageConnection = true)
        {
            if (handleResults == null)
            {
                throw new ArgumentNullException(nameof(handleResults));
            }

            using (command)
            {
                if (manageConnection && command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync(ct).ConfigureAwait(false);
                try
                {
                    using (var reader = await command.ExecuteReaderAsync(commandBehaviour, ct)
                        .ConfigureAwait(false))
                    {
                        var sprocResults = new SprocResults(reader);
                        handleResults(sprocResults);
                    }
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Executes a DbDataReader asynchronously and passes the results thru all <paramref name="resultActions"/>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandBehaviour"></param>
        /// <param name="ct"></param>
        /// <param name="manageConnection"></param>
        /// <param name="resultActions"></param>
        /// <returns></returns>
        public static async Task ExecuteStoredProcAsync(this DbCommand command,
            CommandBehavior commandBehaviour = CommandBehavior.Default,
            CancellationToken ct = default, bool manageConnection = true, params Action<SprocResults>[] resultActions)
        {
            if (resultActions == null)
            {
                throw new ArgumentNullException(nameof(resultActions));
            }

            using (command)
            {
                if (manageConnection && command.Connection.State == ConnectionState.Closed)
                    await command.Connection.OpenAsync(ct).ConfigureAwait(false);
                try
                {
                    using (var reader = await command.ExecuteReaderAsync(commandBehaviour, ct)
                        .ConfigureAwait(false))
                    {
                        var sprocResults = new SprocResults(reader);

                        foreach (var t in resultActions)
                            t(sprocResults);
                    }
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Executes a non-query.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="manageConnection"></param>
        /// <returns></returns>
        public static int ExecuteStoredNonQuery(this DbCommand command, bool manageConnection = true)
        {
            var numberOfRecordsAffected = -1;

            using (command)
            {
                if (command.Connection.State == ConnectionState.Closed)
                {
                    command.Connection.Open();
                }

                try
                {
                    numberOfRecordsAffected = command.ExecuteNonQuery();
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }

            return numberOfRecordsAffected;
        }

        /// <summary>
        /// Executes a non-query asynchronously.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ct"></param>
        /// <param name="manageConnection"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteStoredNonQueryAsync(this DbCommand command, CancellationToken ct = default,
            bool manageConnection = true)
        {
            var numberOfRecordsAffected = -1;

            using (command)
            {
                if (command.Connection.State == ConnectionState.Closed)
                {
                    await command.Connection.OpenAsync(ct).ConfigureAwait(false);
                }

                try
                {
                    numberOfRecordsAffected = await command.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                }
                finally
                {
                    if (manageConnection)
                    {
                        command.Connection.Close();
                    }
                }
            }

            return numberOfRecordsAffected;
        }


        /**************************************************************************************************************************************************
            END EF FLUENT PROC 
        **************************************************************************************************************************************************/

        #endregion


        public static IQueryable<T> InitializeQueryable<T>(this IOltDbContext context)
            where T : class, IOltEntity
        {
            return InitializeQueryable<T>(context, true);
        }

        public static IQueryable<T> InitializeQueryable<T>(this IOltDbContext context, bool includeDeleted)
            where T : class, IOltEntity
        {
            var query = context.Set<T>().AsQueryable();

            if (!includeDeleted)
            {
                query = NonDeletedQueryable(context, query);
            }

            return query;
        }

        public static IQueryable<T> NonDeletedQueryable<T>(this IOltDbContext context, IQueryable<T> queryable)
            where T : class, IOltEntity
        {
            if (typeof(IOltEntityDeletable).IsAssignableFrom(typeof(T)) == false) return queryable;
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


        /// <summary>
        /// modelBuilder.Entity<EF_POCO>().HasQueryFilter(p => p.DeletedOn == null)
        /// https://davecallan.com/entity-framework-core-query-filters-multiple-entities/
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="expression"></param>
        public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetInterface(typeof(TInterface).Name) != null)
                {
                    var newParam = Expression.Parameter(entityType.ClrType);
                    var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newbody, newParam));
                }
            }
        }


        
        /// <summary>
        /// modelBuilder.ApplyGlobalFilters<DateTimeOffset?>("DeletedOn", null)
        /// https://davecallan.com/entity-framework-core-query-filters-multiple-entities/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void ApplyGlobalFilters<T>(this ModelBuilder modelBuilder, string propertyName, T value)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var foundProperty = entityType.FindProperty(propertyName);
                if (foundProperty != null && foundProperty.ClrType == typeof(T))
                {
                    var newParam = Expression.Parameter(entityType.ClrType);
                    var filter = Expression.Lambda(Expression.Equal(Expression.Property(newParam, propertyName), Expression.Constant(value)), newParam);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }
    }
}
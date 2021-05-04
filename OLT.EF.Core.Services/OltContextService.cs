using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OLT.Core
{
    public abstract class OltContextService<TContext> : OltCoreService
        where TContext : class, IOltDbContext
    {
        protected OltContextService(
            IOltServiceManager serviceManager,
            TContext context) : base(serviceManager)
        {
            Context = context;
        }

        protected TContext Context { get; private set; }
        protected int SaveChanges() => Context.SaveChanges();
        //protected Task<int> SaveChangesAsync() => Context.SaveChangesAsync();

        protected IQueryable<TEntity> GetQueryable<TEntity>(params IOltSearcher<TEntity>[] searchers) where TEntity : class, IOltEntity
        {
            var queryable = InitializeQueryable<TEntity>(searchers.Any(p => p.IncludeDeleted));
            searchers.ToList().ForEach(builder =>
            {
                queryable = builder.BuildQueryable(queryable);
            });
            return queryable;
        }

        protected IQueryable<TEntity> GetQueryable<TEntity>(bool includeDeleted) where TEntity : class, IOltEntity
        {
            return GetQueryable(new OltSearcherGetAll<TEntity>(includeDeleted));
        }

        protected virtual IEnumerable<TModel> GetAll<TEntity, TModel>(IOltSearcher<TEntity> searcher, IOltDataAdapter<TEntity, TModel> adapter)
            where TEntity : class, IOltEntity
            where TModel : class, new()
        {
            var queryable = this.GetQueryable(searcher);
            return this.GetAll<TEntity, TModel>(queryable, adapter);
        }

        protected virtual IEnumerable<TModel> GetAll<TEntity, TModel>(IQueryable<TEntity> queryable, IOltDataAdapter<TEntity, TModel> adapter)
            where TEntity : class, IOltEntity
            where TModel : class, new()
        {
            if (adapter is IOltAdapterQueryable<TEntity, TModel> queryableAdapter)
            {
                return queryableAdapter.Map(queryable).ToList();
            }
            return adapter.Map(Include(queryable, adapter).ToList());
        }

        protected virtual IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> queryable, IOltAdapter adapter)
                where TEntity : class, IOltEntity
        {
            if (adapter is IOltDataAdapterQueryableInclude<TEntity> includeAdapter)
            {
                return includeAdapter.Include(queryable);
            }

            return queryable;
        }

        protected virtual TModel Get<TModel, TEntity>(IQueryable<TEntity> queryable, IOltDataAdapter<TEntity, TModel> adapter)
            where TModel : class, new()
            where TEntity : class, IOltEntity
        {
            if (adapter is IOltAdapterQueryable<TEntity, TModel> queryableAdapter)
            {
                return queryableAdapter.Map(queryable).FirstOrDefault();
            }
            var model = new TModel();
            adapter.Map(Include(queryable, adapter).FirstOrDefault(), model);
            return model;
        }

        protected virtual IQueryable<T> Get<T>(IOltSearcher<T> searcher)
            where T : class, IOltEntity
        {
            var query = Context.Set<T>().AsQueryable();

            if (!searcher.IncludeDeleted)
            {
                query = NonDeletedQueryable(query);
            }

            return query;
        }

        protected virtual IQueryable<T> InitializeQueryable<T>()
            where T : class, IOltEntity
        {
            return Context.InitializeQueryable<T>();
        }

        protected virtual IQueryable<T> InitializeQueryable<T>(bool includeDeleted)
            where T : class, IOltEntity
        {
            return Context.InitializeQueryable<T>(includeDeleted);
        }

        protected virtual IQueryable<T> NonDeletedQueryable<T>(IQueryable<T> queryable)
            where T : class, IOltEntity
        {
            return Context.NonDeletedQueryable(queryable);
        }

        protected virtual IQueryable<T> GetQueryable<T>(IOltSearcher<T> queryBuilder)
            where T : class, IOltEntity
        {
            return Context.GetQueryable(queryBuilder);
        }

        protected virtual bool MarkDeleted<T>(T entity)
            where T : class, IOltEntity
        {
            if (entity is IOltEntityDeletable deletableEntity)
            {
                deletableEntity.DeletedOn = DateTimeOffset.Now;
                deletableEntity.DeletedBy = Context.AuditUser;
                SaveChanges();
                return true;
            }

            throw new Exception($"Unable to cast to {nameof(IOltEntityDeletable)}");

        }

    }
}
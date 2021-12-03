using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        protected virtual TContext Context { get; private set; }

        #region [ Save Changes ]

        protected virtual int SaveChanges() => Context.SaveChanges();
        protected virtual async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync(CancellationToken.None);

        #endregion

        #region [ Queryable Methods ]

        protected virtual IQueryable<TEntity> InitializeQueryable<TEntity>() where TEntity : class, IOltEntity
        {
            return InitializeQueryable<TEntity>(false);
        }

        protected virtual IQueryable<TEntity> InitializeQueryable<TEntity>(bool includeDeleted) where TEntity : class, IOltEntity
        {
            return Context.InitializeQueryable<TEntity>(includeDeleted);
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(bool includeDeleted, params IOltSearcher<TEntity>[] searchers) where TEntity : class, IOltEntity
        {
            var queryable = InitializeQueryable<TEntity>(includeDeleted);
            searchers.ToList().ForEach(builder =>
            {
                queryable = builder.BuildQueryable(queryable);
            });
            return queryable;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(bool includeDeleted) where TEntity : class, IOltEntity
        {
            return GetQueryable(new OltSearcherGetAll<TEntity>(includeDeleted));
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(IOltSearcher<TEntity> queryBuilder)
            where TEntity : class, IOltEntity
        {
            return queryBuilder.BuildQueryable(InitializeQueryable<TEntity>(queryBuilder.IncludeDeleted));
        }

        #endregion

        #region [ Get All ]

        protected virtual IEnumerable<TModel> GetAll<TEntity, TModel>(IOltSearcher<TEntity> searcher)
           where TEntity : class, IOltEntity
           where TModel : class, new()
        {
            var queryable = this.GetQueryable(searcher);
            return this.GetAll<TEntity, TModel>(queryable);
        }

        protected virtual IEnumerable<TModel> GetAll<TEntity, TModel>(IQueryable<TEntity> queryable)
            where TEntity : class, IOltEntity
            where TModel : class, new()
        {
            if (ServiceManager.AdapterResolver.CanProjectTo<TEntity, TModel>())
            {
                return ServiceManager.AdapterResolver.ProjectTo<TEntity, TModel>(queryable).ToList();
            }

            var entities = ServiceManager.AdapterResolver.Include<TEntity, TModel>(queryable).ToList();
            return ServiceManager.AdapterResolver.Map<TEntity, TModel>(entities);
        }

        protected virtual async Task<IEnumerable<TModel>> GetAllAsync<TEntity, TModel>(IOltSearcher<TEntity> searcher)
            where TEntity : class, IOltEntity
            where TModel : class, new()
        {
            var queryable = this.GetQueryable(searcher);
            return await this.GetAllAsync<TEntity, TModel>(queryable);
        }

        protected virtual async Task<IEnumerable<TModel>> GetAllAsync<TEntity, TModel>(IQueryable<TEntity> queryable)
            where TEntity : class, IOltEntity
            where TModel : class, new()
        {
            if (ServiceManager.AdapterResolver.CanProjectTo<TEntity, TModel>())
            {
                return await ServiceManager.AdapterResolver.ProjectTo<TEntity, TModel>(queryable).ToListAsync();
            }

            var entities = await ServiceManager.AdapterResolver.Include<TEntity, TModel>(queryable).ToListAsync();
            return ServiceManager.AdapterResolver.Map<TEntity, TModel>(entities);
        }


        #endregion

        #region [ Get ]

        protected virtual TModel Get<TEntity, TModel>(IQueryable<TEntity> queryable)
            where TModel : class, new()
            where TEntity : class, IOltEntity
        {

            if (ServiceManager.AdapterResolver.CanProjectTo<TEntity, TModel>())
            {
                return ServiceManager.AdapterResolver.ProjectTo<TEntity, TModel>(queryable).FirstOrDefault();
            }

            var model = new TModel();
            var entity = ServiceManager.AdapterResolver.Include<TEntity, TModel>(queryable).FirstOrDefault();
            return ServiceManager.AdapterResolver.Map(entity, model);
        }

        protected virtual async Task<TModel> GetAsync<TEntity, TModel>(IQueryable<TEntity> queryable)
            where TModel : class, new()
            where TEntity : class, IOltEntity
        {

            if (ServiceManager.AdapterResolver.CanProjectTo<TEntity, TModel>())
            {
                return await ServiceManager.AdapterResolver.ProjectTo<TEntity, TModel>(queryable).FirstOrDefaultAsync();
            }

            var model = new TModel();
            var entity = await ServiceManager.AdapterResolver.Include<TEntity, TModel>(queryable).FirstOrDefaultAsync();
            return ServiceManager.AdapterResolver.Map(entity, model);
        }


        #endregion

        #region [ Mark Deleted ]

        protected virtual bool MarkDeleted<TEntity>(TEntity entity)
            where TEntity : class, IOltEntity
        {
            if (entity is IOltEntityDeletable deletableEntity)
            {
                deletableEntity.DeletedOn = DateTimeOffset.Now;
                deletableEntity.DeletedBy = Context.AuditUser;
                SaveChanges();
                return true;
            }

            throw new InvalidCastException($"Unable to cast to {nameof(IOltEntityDeletable)}");

        }

        protected virtual async Task<bool> MarkDeletedAsync<TEntity>(TEntity entity)
            where TEntity : class, IOltEntity
        {
            if (entity is IOltEntityDeletable deletableEntity)
            {
                deletableEntity.DeletedOn = DateTimeOffset.Now;
                deletableEntity.DeletedBy = Context.AuditUser;
                await SaveChangesAsync();
                return true;
            }

            throw new InvalidCastException($"Unable to cast to {nameof(IOltEntityDeletable)}");

        }


        #endregion

    }
}
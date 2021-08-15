using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public abstract class OltEntityService<TContext, TEntity> : OltContextService<TContext>, IOltEntityService<TEntity>
     where TEntity : class, IOltEntity, new()
     where TContext : DbContext, IOltDbContext
    {

        protected OltEntityService(
            IOltServiceManager serviceManager,
            TContext context) : base(serviceManager, context)
        {
        }

        protected virtual DbSet<TEntity> Repository => Context.Set<TEntity>();
        protected virtual IQueryable<TEntity> GetQueryable() => Repository;

        protected virtual IQueryable<TEntity> GetQueryable(IOltSearcher<TEntity> searcher)
        {
            return searcher.BuildQueryable(InitializeQueryable<TEntity>(searcher.IncludeDeleted));
        }


        //protected IEnumerable<TModel> GetAll<TModel>(IQueryable<TEntity> queryable, IOltAdapter<TEntity, TModel> adapter)
        //    where TModel : class, new()
        //{
        //    if (adapter is IOltAdapterQueryable<TEntity, TModel> queryableAdapter)
        //    {
        //        return queryableAdapter.Map(queryable).ToList();
        //    }
        //    return adapter.Map(Include(queryable, adapter).ToList());
        //}



        protected virtual IEnumerable<TModel> GetAll<TModel>(IQueryable<TEntity> queryable) where TModel : class, new()
        {
            return base.GetAll<TEntity, TModel>(queryable);
        }


        protected virtual TModel Get<TModel>(IQueryable<TEntity> queryable) where TModel : class, new()
        {
            return base.Get<TEntity, TModel>(queryable);
        }

        public virtual TModel Upsert<TModel>(IOltSearcher<TEntity> searcher, TModel model) where TModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.Include<TEntity, TModel>(GetQueryable(searcher)).FirstOrDefault();

            if (entity == null)
            {
                entity = new TEntity();
                Repository.Add(entity);
            }

            ServiceManager.AdapterResolver.Map(model, entity);


            SaveChanges();
            return Get<TModel>(searcher);
        }


        public virtual IEnumerable<TModel> Find<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class, new()
        {
            var query = (Expression<Func<TEntity, bool>>)OltRemoveCastsVisitor.Visit(predicate);
            var queryable = this.Repository.Where(query);
            return GetAll<TModel>(queryable);
        }


        public virtual IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> queryBuilder, IOltPagingParams pagingParams)
            where TModel : class, new()
        {
            return this.GetPaged<TModel>(GetQueryable(queryBuilder), pagingParams);
        }

        protected virtual IOltPaged<TModel> GetPaged<TModel>(IQueryable<TEntity> queryable, IOltPagingParams pagingParams)
           where TModel : class, new()
        {
            return ServiceManager.AdapterResolver.Paged<TEntity, TModel>(queryable, pagingParams);
        }

        public virtual IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> queryBuilder, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
            where TModel : class, new()
        {
            return this.GetPaged<TModel>(GetQueryable(queryBuilder), pagingParams, orderBy);
        }

        public virtual IOltPaged<TModel> GetPaged<TModel>(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
            where TModel : class, new()
        {
            return ServiceManager.AdapterResolver.Paged<TEntity, TModel>(queryable, pagingParams, orderBy);
        }

        public virtual TModel Add<TModel>(TModel model)
                   where TModel : class, new()
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            Repository.Add(entity);
            SaveChanges();
            var response = new TModel();
            ServiceManager.AdapterResolver.Map(entity, response);
            return response;
        }

        public virtual TResponseModel Add<TResponseModel, TSaveModel>(TSaveModel model)
            where TSaveModel : class, new()
            where TResponseModel : class, new()
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            Repository.Add(entity);
            SaveChanges();
            var response = new TResponseModel();
            ServiceManager.AdapterResolver.Map(entity, response);
            return response;
        }

        public virtual IEnumerable<TResponseModel> Add<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> list)
            where TSaveModel : class, new()
            where TResponseModel : class, new()
        {
            var entities = new List<TEntity>();
            list.ToList().ForEach(model =>
            {
                var entity = new TEntity();
                ServiceManager.AdapterResolver.Map(model, entity);
                Repository.Add(entity);
                entities.Add(entity);
            });

            SaveChanges();
            var returnList = new List<TResponseModel>();
            entities.ForEach(entity =>
            {
                var response = new TResponseModel();
                ServiceManager.AdapterResolver.Map(entity, response);
                returnList.Add(response);
            });
            return returnList;
        }

        public TModel Update<TModel>(IOltSearcher<TEntity> queryBuilder, TModel model) where TModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.Include<TEntity, TModel>(GetQueryable(queryBuilder)).FirstOrDefault();
            ServiceManager.AdapterResolver.Map(model, entity);
            SaveChanges();
            var response = new TModel();
            ServiceManager.AdapterResolver.Map(entity, response);
            return response;
        }

        public virtual TModel Get<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new()
            => this.Get<TModel>(GetQueryable(searcher));

        public virtual TModel Get<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new()
            => this.Get<TModel>(GetQueryable(searchers));

        public virtual IEnumerable<TModel> GetAll<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new()
            => this.GetAll<TModel>(GetQueryable(searcher));

        public virtual IEnumerable<TModel> GetAll<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new()
            => this.GetAll<TModel>(GetQueryable(searchers));

        public virtual bool SoftDelete(IOltSearcher<TEntity> queryBuilder)
        {
            var entity = GetQueryable(queryBuilder).FirstOrDefault();
            return entity != null && MarkDeleted(entity);
        }

    }
}

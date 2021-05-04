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

        protected virtual IQueryable<TEntity> Include(IQueryable<TEntity> queryable, IOltAdapter adapter)
        {
            if (adapter is IOltDataAdapterQueryableInclude<TEntity> includeAdapter)
            {
                return includeAdapter.Include(queryable);
            }

            return queryable;
        }

        protected IEnumerable<TModel> GetAll<TModel>(IQueryable<TEntity> queryable, IOltDataAdapter<TEntity, TModel> adapter)
            where TModel : class, new()
        {
            if (adapter is IOltAdapterQueryable<TEntity, TModel> queryableAdapter)
            {
                return queryableAdapter.Map(queryable).ToList();
            }
            return adapter.Map(Include(queryable, adapter).ToList());
        }

        protected IEnumerable<TModel> GetAll<TModel>(IOltSearcher<TEntity> searcher, IOltDataAdapter<TEntity, TModel> adapter)
            where TModel : class, new()
        {
            var queryable = this.GetQueryable(searcher);
            return this.GetAll<TModel>(queryable, adapter);
        }

        protected virtual IEnumerable<TModel> GetAll<TModel>(IQueryable<TEntity> queryable) where TModel : class, new()
        {
            var adapter = ServiceManager.AdapterResolver.GetAdapter<TEntity, TModel>();
            return this.GetAll<TModel>(queryable, adapter);
        }


        protected virtual TModel Get<TModel>(IOltSearcher<TEntity> searcher, IOltDataAdapter<TEntity, TModel> adapter) where TModel : class, new()
        {
            return Get(GetQueryable(searcher), adapter);
        }

        protected virtual TModel Get<TModel>(IQueryable<TEntity> queryable, IOltDataAdapter<TEntity, TModel> adapter) where TModel : class, new()
        {
            return base.Get(queryable, adapter);
        }

        protected virtual TModel Get<TModel>(IQueryable<TEntity> queryable) where TModel : class, new()
        {
            return base.Get(queryable, ServiceManager.AdapterResolver.GetAdapter<TEntity, TModel>());
        }

        public virtual TModel Upsert<TModel>(IOltSearcher<TEntity> searcher, TModel model) where TModel : class, new()
        {
            var adapter = ServiceManager.AdapterResolver.GetAdapter<TEntity, TModel>();
            var entity = Include(GetQueryable(searcher), adapter).FirstOrDefault();

            if (entity == null)
            {
                //entity = Repository.Create();
                //entity = Repository.CreateProxy();
                entity = new TEntity();
                Repository.Add(entity);
            }

            adapter.Map(model, entity);


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

        protected virtual IOltPaged<TModel> GetPaged<TModel>(
           IQueryable<TEntity> queryable,
           IOltPagingParams pagingParams)
           where TModel : class, new()
        {
            IOltAdapterPaged<TEntity, TModel> pagedAdapter = ServiceManager.AdapterResolver.GetPagedAdapter<TEntity, TModel>();
            if (pagingParams is IOltPagingWithSortParams pagingWithSortParams)
                return pagedAdapter.Map(queryable, pagingParams, pagingWithSortParams);
            return pagedAdapter.Map(queryable, pagingParams);
        }

        public virtual IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> queryBuilder, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
            where TModel : class, new()
        {
            return this.GetPaged<TModel>(GetQueryable(queryBuilder), pagingParams, orderBy);
        }

        public virtual IOltPaged<TModel> GetPaged<TModel>(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
            where TModel : class, new()
        {
            IOltAdapterPaged<TEntity, TModel> pagedAdapter = ServiceManager.AdapterResolver.GetPagedAdapter<TEntity, TModel>();
            return pagedAdapter.Map(queryable, pagingParams, orderBy);
        }

        public virtual TModel Add<TModel>(TModel model)
                   where TModel : class, new()
        {
            var adapter = ServiceManager.AdapterResolver.GetAdapter<TEntity, TModel>();
            //var entity = Repository.CreateProxy();
            var entity = new TEntity();
            adapter.Map(model, entity);
            Repository.Add(entity);
            SaveChanges();
            var response = new TModel();
            adapter.Map(entity, response);
            return response;
        }

        public virtual TResponseModel Add<TResponseModel, TSaveModel>(TSaveModel model)
            where TSaveModel : class, new()
            where TResponseModel : class, new()
        {
            var adapter = ServiceManager.AdapterResolver.GetAdapter<TEntity, TSaveModel>();
            var entity = new TEntity();
            //var entity = Repository.CreateProxy();
            adapter.Map(model, entity);
            Repository.Add(entity);
            SaveChanges();
            var adapterResponse = ServiceManager.AdapterResolver.GetAdapter<TEntity, TResponseModel>();
            var response = new TResponseModel();
            adapterResponse.Map(entity, response);
            return response;
        }

        public virtual IEnumerable<TResponseModel> Add<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> list)
            where TSaveModel : class, new()
            where TResponseModel : class, new()
        {
            var adapter = ServiceManager.AdapterResolver.GetAdapter<TEntity, TSaveModel>();
            var entities = new List<TEntity>();
            list.ToList().ForEach(model =>
            {
                var entity = new TEntity();
                //var entity = Repository.CreateProxy();
                adapter.Map(model, entity);
                Repository.Add(entity);
                entities.Add(entity);
            });

            SaveChanges();
            var adapterResponse = ServiceManager.AdapterResolver.GetAdapter<TEntity, TResponseModel>();
            var returnList = new List<TResponseModel>();
            entities.ForEach(entity =>
            {
                var response = new TResponseModel();
                adapterResponse.Map(entity, response);
                returnList.Add(response);
            });
            return returnList;
        }

        public TModel Update<TModel>(IOltSearcher<TEntity> queryBuilder, TModel model) where TModel : class, new()
        {
            var adapter = ServiceManager.AdapterResolver.GetAdapter<TEntity, TModel>();
            var entity = Include(GetQueryable(queryBuilder), adapter).FirstOrDefault();
            adapter.Map(model, entity);
            SaveChanges();
            var response = new TModel();
            adapter.Map(entity, response);
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

        //public IOltFileBase64 Export(IOltFileExportUtility exportUtility,  IOltSearcher<TEntity> queryBuilder, string exporterName)
        //{
                
        //}
    }
}

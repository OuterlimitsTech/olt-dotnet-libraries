using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        #region [ Get Queryable ]

        protected virtual IQueryable<TEntity> GetQueryable() => Repository;

        protected virtual IQueryable<TEntity> GetQueryable(IOltSearcher<TEntity> searcher)
        {
            return searcher.BuildQueryable(Context.InitializeQueryable<TEntity>(searcher.IncludeDeleted));
        }

        #endregion

        #region [ Get ]

        protected virtual TModel Get<TModel>(IQueryable<TEntity> queryable) where TModel : class, new()
        {
            return base.Get<TEntity, TModel>(queryable);
        }

        protected virtual async Task<TModel> GetAsync<TModel>(IQueryable<TEntity> queryable) where TModel : class, new()
        {
            return await base.GetAsync<TEntity, TModel>(queryable);
        }

        public virtual TModel Get<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new()
            => this.Get<TModel>(GetQueryable(searcher));

        public virtual TModel Get<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new()
            => this.Get<TModel>(GetQueryable(searchers));

        public virtual async Task<TModel> GetAsync<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new()
            => await this.GetAsync<TModel>(GetQueryable(searcher));

        public virtual async Task<TModel> GetAsync<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new()
            => await this.GetAsync<TModel>(GetQueryable(searchers));

        #endregion

        #region [ Get All ]

        protected virtual IEnumerable<TModel> GetAll<TModel>(IQueryable<TEntity> queryable) where TModel : class, new()
        {
            return base.GetAll<TEntity, TModel>(queryable);
        }

        protected virtual async Task<IEnumerable<TModel>> GetAllAsync<TModel>(IQueryable<TEntity> queryable) where TModel : class, new()
        {
            return await base.GetAllAsync<TEntity, TModel>(queryable);
        }

        public virtual IEnumerable<TModel> GetAll<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new()
            => this.GetAll<TModel>(GetQueryable(searcher));

        public virtual IEnumerable<TModel> GetAll<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new()
            => this.GetAll<TModel>(GetQueryable(searchers));

        public virtual async Task<IEnumerable<TModel>> GetAllAsync<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new()
            => await this.GetAllAsync<TModel>(GetQueryable(searcher));

        public virtual async Task<IEnumerable<TModel>> GetAllAsync<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new()
            => await this.GetAllAsync<TModel>(GetQueryable(searchers));


        #endregion
        
        #region [ Find ] 

        public virtual IEnumerable<TModel> Find<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class, new()
        {
            var query = (Expression<Func<TEntity, bool>>)OltRemoveCastsVisitor.Visit(predicate);
            var queryable = this.Repository.Where(query);
            return GetAll<TModel>(queryable);
        }

        public virtual async Task<IEnumerable<TModel>> FindAsync<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class, new()
        {
            var query = (Expression<Func<TEntity, bool>>)OltRemoveCastsVisitor.Visit(predicate);
            var queryable = this.Repository.Where(query);
            return await GetAllAsync<TModel>(queryable);
        }

        #endregion

        #region [ Get Paged ]

        public virtual IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> searcher, IOltPagingParams pagingParams)
            where TModel : class, new()
        {
            return this.GetPaged<TModel>(GetQueryable(searcher), pagingParams);
        }

        protected virtual IOltPaged<TModel> GetPaged<TModel>(IQueryable<TEntity> queryable, IOltPagingParams pagingParams)
           where TModel : class, new()
        {
            return ServiceManager.AdapterResolver.Paged<TEntity, TModel>(queryable, pagingParams);
        }

        public virtual IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> searcher, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
            where TModel : class, new()
        {
            return this.GetPaged<TModel>(GetQueryable(searcher), pagingParams, orderBy);
        }

        public virtual IOltPaged<TModel> GetPaged<TModel>(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
            where TModel : class, new()
        {
            return ServiceManager.AdapterResolver.Paged<TEntity, TModel>(queryable, pagingParams, orderBy);
        }

        public virtual async Task<IOltPaged<TModel>> GetPagedAsync<TModel>(IOltSearcher<TEntity> searcher, IOltPagingParams pagingParams)
            where TModel : class, new()
        {
            return await GetPagedAsync<TModel>(GetQueryable(searcher), pagingParams, ServiceManager.AdapterResolver.DefaultOrderBy<TEntity, TModel>());
        }

        public virtual async Task<IOltPaged<TModel>> GetPagedAsync<TModel>(IQueryable<TEntity> queryable, IOltPagingParams pagingParams)
            where TModel : class, new()
        {
            return await GetPagedAsync<TModel>(queryable, pagingParams, ServiceManager.AdapterResolver.DefaultOrderBy<TEntity, TModel>());
        }

        public virtual async Task<IOltPaged<TModel>> GetPagedAsync<TModel>(IQueryable<TEntity> source, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
            where TModel : class, new()
        {
            var mapped = ServiceManager.AdapterResolver.ProjectTo<TEntity, TModel>(orderBy(source));
            return await mapped.ToPagedAsync(pagingParams);
        }

        #endregion

        #region [ Add List ]

        protected virtual List<TEntity> AddFromList<TModel>(List<TModel> list) where TModel : class, new()
        {
            var entities = new List<TEntity>();
            list.ForEach(item =>
            {
                var entity = new TEntity();
                ServiceManager.AdapterResolver.Map(item, entity);
                entities.Add(entity);
                Repository.Add(entity);
            });
            SaveChanges();
            return entities;
        }

        protected virtual async Task<List<TEntity>> AddFromListAsync<TModel>(List<TModel> list) where TModel : class, new()
        {
            var entities = new List<TEntity>();
            await list.ForEachAsync(async item =>
            {
                var entity = new TEntity();
                ServiceManager.AdapterResolver.Map(item, entity);
                entities.Add(entity);
                await Repository.AddAsync(entity);
            });
            await SaveChangesAsync();
            return entities;
        }

        #endregion

        #region [ Build Result List ]

        protected virtual List<TModel> BuildResultList<TModel>(List<TEntity> entities) where TModel : class, new()
        {
            var returnList = new List<TModel>();
            entities.ForEach(entity =>
            {
                var response = new TModel();
                ServiceManager.AdapterResolver.Map(entity, response);
                returnList.Add(response);
            });
            return returnList;
        }

        #endregion

        #region [ Add  ]

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


        public virtual List<TModel> Add<TModel>(List<TModel> list) where TModel : class, new()
        {
            return BuildResultList<TModel>(AddFromList(list));
        }

        public virtual IEnumerable<TModel> Add<TModel>(IEnumerable<TModel> collection) where TModel : class, new()
        {
            return Add(collection.ToList());
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
            return BuildResultList<TResponseModel>(AddFromList(list.ToList()));
        }


        public virtual async Task<TModel> AddAsync<TModel>(TModel model) where TModel : class, new()
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            await Repository.AddAsync(entity);
            await SaveChangesAsync();
            var response = new TModel();
            ServiceManager.AdapterResolver.Map(entity, response);
            return response;
        }

        public async Task<List<TModel>> AddAsync<TModel>(List<TModel> list) where TModel : class, new()
        {
            return BuildResultList<TModel>(await AddFromListAsync(list));
        }

        public async Task<IEnumerable<TModel>> AddAsync<TModel>(IEnumerable<TModel> collection) where TModel : class, new()
        {
            return await AddAsync(collection.ToList());
        }

        public virtual async Task<TResponseModel> AddAsync<TResponseModel, TSaveModel>(TSaveModel model)
            where TSaveModel : class, new()
            where TResponseModel : class, new()
        {
            var entity = new TEntity();
            ServiceManager.AdapterResolver.Map(model, entity);
            await Repository.AddAsync(entity);
            await SaveChangesAsync();
            var response = new TResponseModel();
            ServiceManager.AdapterResolver.Map(entity, response);
            return response;
        }

        public virtual async Task<List<TResponseModel>> AddAsync<TResponseModel, TSaveModel>(List<TSaveModel> list) 
            where TResponseModel : class, new()
            where TSaveModel : class, new()
        {
            return BuildResultList<TResponseModel>(await AddFromListAsync(list.ToList()));
        }

        public virtual async Task<IEnumerable<TResponseModel>> AddAsync<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> collection) where TResponseModel : class, new() where TSaveModel : class, new()
        {
            return await this.AddAsync<TResponseModel, TSaveModel>(collection.ToList());
        }

        #endregion
        
        #region [ Update ]
        

        public virtual TModel Update<TModel>(IOltSearcher<TEntity> searcher, TModel model) where TModel : class, new()
        {
            var entity = ServiceManager.AdapterResolver.Include<TEntity, TModel>(GetQueryable(searcher)).FirstOrDefault();
            ServiceManager.AdapterResolver.Map(model, entity);
            SaveChanges();
            return Get<TModel>(searcher);
        }

        public virtual async Task<TModel> UpdateAsync<TModel>(IOltSearcher<TEntity> searcher, TModel model) where TModel : class, new()
        {
            var entity = await ServiceManager.AdapterResolver.Include<TEntity, TModel>(GetQueryable(searcher)).FirstOrDefaultAsync();
            ServiceManager.AdapterResolver.Map(model, entity);
            await SaveChangesAsync();
            return await GetAsync<TModel>(searcher);
        }

        #endregion

        #region [ Soft Delete ]

        public virtual bool SoftDelete(IOltSearcher<TEntity> searcher)
        {
            var entity = GetQueryable(searcher).FirstOrDefault();
            return entity != null && MarkDeleted(entity);
        }

        public virtual async Task<bool> SoftDeleteAsync(IOltSearcher<TEntity> searcher)
        {
            var entity = await GetQueryable(searcher).FirstOrDefaultAsync();
            return entity != null && await MarkDeletedAsync(entity);
        }

        #endregion

        #region [ Count ]

        public virtual int Count(IOltSearcher<TEntity> searcher)
        {
            return GetQueryable(searcher).Count();
        }

        public virtual async Task<int> CountAsync(IOltSearcher<TEntity> searcher)
        {
            return await GetQueryable(searcher).CountAsync();
        }

        #endregion


    }
}

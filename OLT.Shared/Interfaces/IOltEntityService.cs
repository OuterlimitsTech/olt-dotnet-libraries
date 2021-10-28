using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OLT.Core
{
    public interface IOltEntityService<TEntity> : IOltCoreService
        where TEntity : class, IOltEntity
    {
        IEnumerable<TModel> GetAll<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new();
        IEnumerable<TModel> GetAll<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new();
        IEnumerable<TModel> GetAll<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class, new();

        Task<IEnumerable<TModel>> GetAllAsync<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new();
        Task<IEnumerable<TModel>> GetAllAsync<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new();
        Task<IEnumerable<TModel>> GetAllAsync<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class, new();

        TModel Get<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new();
        TModel Get<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class, new();
        TModel Get<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new();

        Task<TModel> GetAsync<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class, new();
        Task<TModel> GetAsync<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new();
        Task<TModel> GetAsync<TModel>(params IOltSearcher<TEntity>[] searchers) where TModel : class, new();

       IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> searcher, IOltPagingParams pagingParams) where TModel : class, new();
       IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> searcher, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy) where TModel : class, new();

        Task<IOltPaged<TModel>> GetPagedAsync<TModel>(IOltSearcher<TEntity> searcher, IOltPagingParams pagingParams)
            where TModel : class, new();

        //Task<IOltPaged<TModel>> GetPagedAsync<TModel>(IQueryable<TEntity> queryable, IOltPagingParams pagingParams)
        //    where TModel : class, new();

        //Task<IOltPaged<TModel>> GetPagedAsync<TModel>(IQueryable<TEntity> source, IOltPagingParams pagingParams,
        //    Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy)
        //    where TModel : class, new();

        TModel Add<TModel>(TModel model) where TModel : class, new();
        List<TModel> Add<TModel>(List<TModel> list) where TModel : class, new();
        IEnumerable<TModel> Add<TModel>(IEnumerable<TModel> collection) where TModel : class, new();

        TResponseModel Add<TResponseModel, TSaveModel>(TSaveModel model)
            where TResponseModel : class, new()
            where TSaveModel : class, new();

        IEnumerable<TResponseModel> Add<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> list)
            where TSaveModel : class, new()
            where TResponseModel : class, new();

        Task<TModel> AddAsync<TModel>(TModel model) where TModel : class, new();
        Task<List<TModel>> AddAsync<TModel>(List<TModel> list) where TModel : class, new();
        Task<IEnumerable<TModel>> AddAsync<TModel>(IEnumerable<TModel> collection) where TModel : class, new();

        Task<TResponseModel> AddAsync<TResponseModel, TSaveModel>(TSaveModel model)
            where TResponseModel : class, new()
            where TSaveModel : class, new();

        Task<List<TResponseModel>> AddAsync<TResponseModel, TSaveModel>(List<TSaveModel> list)
            where TResponseModel : class, new()
            where TSaveModel : class, new();

        Task<IEnumerable<TResponseModel>> AddAsync<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> collection)
            where TSaveModel : class, new()
            where TResponseModel : class, new();

        TModel Update<TModel>(IOltSearcher<TEntity> searcher, TModel model) where TModel : class, new();
        Task<TModel> UpdateAsync<TModel>(IOltSearcher<TEntity> searcher, TModel model) where TModel : class, new();


        bool SoftDelete(IOltSearcher<TEntity> searcher);
        Task<bool> SoftDeleteAsync(IOltSearcher<TEntity> searcher);
        
        int Count(IOltSearcher<TEntity> searcher);
        Task<int> CountAsync(IOltSearcher<TEntity> searcher);
    }
}
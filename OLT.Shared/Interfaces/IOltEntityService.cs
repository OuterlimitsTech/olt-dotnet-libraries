using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OLT.Core
{
    public interface IOltEntityService<TEntity> : IOltCoreService
        where TEntity : class, IOltEntity
    {
        IEnumerable<TModel> GetAll<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new();
        TModel Get<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new();
        IEnumerable<TModel> Find<TModel>(Expression<Func<TEntity, bool>> predicate) where TModel : class, new();
        IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> queryBuilder, IOltPagingParams pagingParams) where TModel : class, new();
        IOltPaged<TModel> GetPaged<TModel>(IOltSearcher<TEntity> queryBuilder, IOltPagingParams pagingParams,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy) where TModel : class, new();


        TModel Add<TModel>(TModel model) where TModel : class, new();

        TResponseModel Add<TResponseModel, TSaveModel>(TSaveModel model)
            where TResponseModel : class, new()
            where TSaveModel : class, new();

        IEnumerable<TResponseModel> Add<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> list)
            where TSaveModel : class, new()
            where TResponseModel : class, new();

        TModel Upsert<TModel>(IOltSearcher<TEntity> searcher, TModel model) where TModel : class, new();
        TModel Update<TModel>(IOltSearcher<TEntity> queryBuilder, TModel model) where TModel : class, new();
        bool SoftDelete(IOltSearcher<TEntity> queryBuilder);

    }
}
using System;
using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterPaged<TEntity, TModel> : IOltAdapterQueryable<TEntity, TModel>
        where TEntity : class, IOltEntity
        where TModel : class, new()
    {
        IOltPaged<TModel> Map(IQueryable<TEntity> queryable, IOltPagingWithSortParams pagingParams);
        IOltPaged<TModel> Map(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, IOltSortParams sortParams = null);
        IOltPaged<TModel> Map(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy);
    }
}
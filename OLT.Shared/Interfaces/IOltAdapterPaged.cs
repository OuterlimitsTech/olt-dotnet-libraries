using System;
using System.Linq;

namespace OLT.Core
{
    //TODO: Move to DataAdapter package
    public interface IOltAdapterPaged<TEntity, TModel> : IOltAdapterQueryable<TEntity, TModel>
    {
        IOltPaged<TModel> Map(IQueryable<TEntity> queryable, IOltPagingWithSortParams pagingParams);
        IOltPaged<TModel> Map(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, IOltSortParams sortParams = null);
        IOltPaged<TModel> Map(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy);
    }
}
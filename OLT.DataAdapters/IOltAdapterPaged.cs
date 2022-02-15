using System;
using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterPaged<TEntity, TDestination> : IOltAdapterQueryable<TEntity, TDestination>
    {
        IQueryable<TEntity> DefaultOrderBy(IQueryable<TEntity> queryable);
        IOltPaged<TDestination> Map(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, IOltSortParams sortParams = null);
        IOltPaged<TDestination> Map(IQueryable<TEntity> queryable, IOltPagingParams pagingParams, Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy);
    }
}
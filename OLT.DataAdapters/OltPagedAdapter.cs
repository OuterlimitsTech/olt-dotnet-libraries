using System;
using System.Linq;

namespace OLT.Core
{
    public abstract class OltAdapterPaged<TSource, TDestination> : OltAdapterQueryable<TSource, TDestination>, IOltAdapterPaged<TSource, TDestination>
        where TSource : class, IOltEntity, new()
        where TDestination : class, new()
    {

        public abstract IQueryable<TSource> DefaultOrderBy(IQueryable<TSource> queryable);

        public virtual IQueryable<TSource> OrderBy(IQueryable<TSource> queryable, IOltSortParams sortParams = null)
        {
            return queryable.OrderBy(sortParams, DefaultOrderBy);
        }

        public virtual IOltPaged<TDestination> Map(IQueryable<TSource> queryable, IOltPagingWithSortParams pagingParams)
        {
            return Map(queryable, pagingParams, pagingParams);
        }

        public virtual IOltPaged<TDestination> Map(IQueryable<TSource> queryable, IOltPagingParams pagingParams, IOltSortParams sortParams = null)
        {
            return this.Map(queryable, pagingParams, sortQueryable => OrderBy(sortQueryable, sortParams));
        }

        public virtual IOltPaged<TDestination> Map(IQueryable<TSource> queryable, IOltPagingParams pagingParams, Func<IQueryable<TSource>, IQueryable<TSource>> orderBy)
        {
            var mapped = OltAfterMapConfig.ApplyAfterMaps<TSource, TDestination>(Map(orderBy(queryable)));
            return mapped.ToPaged(pagingParams);
        }

    }
}
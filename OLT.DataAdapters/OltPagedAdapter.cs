using System;
using System.Linq;

namespace OLT.Core
{
    public abstract class OltAdapterPaged<TSource, TModel> : OltAdapterQueryable<TSource, TModel>, IOltAdapterPaged<TSource, TModel>
        where TSource : class, IOltEntity, new()
        where TModel : class, new()
    {


        public virtual IOltPaged<TModel> Map(IQueryable<TSource> queryable, IOltPagingWithSortParams pagingParams)
        {
            return Map(queryable, pagingParams, pagingParams);
        }

        public abstract IQueryable<TSource> DefaultOrderBy(IQueryable<TSource> queryable);

        public virtual IQueryable<TSource> OrderBy(IQueryable<TSource> queryable, IOltSortParams sortParams = null)
        {
            return queryable.OrderBy(sortParams, DefaultOrderBy);
        }

        public virtual IOltPaged<TModel> Map(IQueryable<TSource> queryable, IOltPagingParams pagingParams, IOltSortParams sortParams = null)
        {
            return this.Map(queryable, pagingParams, sortQueryable => OrderBy(sortQueryable, sortParams));
        }

        public virtual IOltPaged<TModel> Map(IQueryable<TSource> queryable, IOltPagingParams pagingParams, Func<IQueryable<TSource>, IQueryable<TSource>> orderBy)
        {
            var mapped = Map(orderBy(queryable));
            return mapped.ToPaged(pagingParams);
        }

    }
}
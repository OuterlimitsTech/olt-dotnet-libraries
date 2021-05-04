////using System;
////using System.Linq;

////namespace OLT.Core
////{
////    public abstract class OltAdapterPaged<TSource, TModel> : OltAdapterQueryable<TSource, TModel>, IOltAdapterPaged<TSource, TModel>
////        where TSource : class, IOltEntity, new()
////        where TModel : class, new()
////    {


////        public virtual IOltPaged<TModel> Map(IQueryable<TSource> queryable, IOltPagingWithSortParams pagingParams)
////        {
////            return Map(queryable, pagingParams, pagingParams);
////        }

////        public abstract IQueryable<TSource> DefaultOrderBy(IQueryable<TSource> queryable);

////        public virtual IQueryable<TSource> OrderBy(IQueryable<TSource> queryable, IOltSortParams sortParams = null)
////        {
////            var orderedQueryable = sortParams?.PropertyName != null && !string.IsNullOrEmpty(sortParams.PropertyName)
////                ? queryable.OrderBy(sortParams)
////                : queryable;
////            return DefaultOrderBy(orderedQueryable);
////        }

////        public virtual IOltPaged<TModel> Map(IQueryable<TSource> queryable, IOltPagingParams pagingParams, IOltSortParams sortParams = null)
////        {
////            var cnt = queryable.Count();

////            queryable = OrderBy(queryable, sortParams);

////            var pagedQueryable = this.Map(queryable)
////                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
////                .Take(pagingParams.PageSize);


////            return new OltPagedData<TModel>
////            {
////                Count = cnt,
////                PageNumber = pagingParams.PageNumber,
////                PageSize = pagingParams.PageSize,
////                Data = pagedQueryable.ToList()
////            };
////        }

////        public virtual IOltPaged<TModel> Map(IQueryable<TSource> queryable, IOltPagingParams pagingParams, Func<IQueryable<TSource>, IQueryable<TSource>> orderBy)
////        {
////            var cnt = queryable.Count();

////            queryable = orderBy(queryable);

////            var pagedQueryable = this.Map(queryable)
////                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
////                .Take(pagingParams.PageSize);


////            return new OltPagedData<TModel>
////            {
////                Count = cnt,
////                PageNumber = pagingParams.PageNumber,
////                PageSize = pagingParams.PageSize,
////                Data = pagedQueryable.ToList()
////            };
////        }

////    }
////}
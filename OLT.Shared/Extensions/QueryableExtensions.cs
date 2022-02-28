using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    public static class QueryableExtensions
    {

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, IOltSortParams sortParams)
        {
            return source.OrderByPropertyName(sortParams.PropertyName, sortParams.IsAscending);
        }

        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> queryable, IOltSortParams sortParams, Func<IQueryable<TSource>, IQueryable<TSource>> defaultOrderBy)
        {
            var orderedQueryable = sortParams?.PropertyName != null && !string.IsNullOrEmpty(sortParams.PropertyName)
                ? queryable.OrderBy(sortParams)
                : queryable;
            return defaultOrderBy(orderedQueryable);
        }

        public static IOltPaged<TDestination> ToPaged<TDestination>(this IQueryable<TDestination> queryable, IOltPagingParams pagingParams)
        {
            var cnt = queryable.Count();

            var pagedQueryable = queryable
                .Skip((pagingParams.Page - 1) * pagingParams.Size)
                .Take(pagingParams.Size);


            return new OltPagedJson<TDestination>
            {
                Count = cnt,
                Page = pagingParams.Page,
                Size = pagingParams.Size,
                Data = pagedQueryable.ToList()
            };

        }


    }
}
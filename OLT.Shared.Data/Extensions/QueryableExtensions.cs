////using System.Collections.Generic;
////using System.Linq;
////using System.Linq.Expressions;

////// ReSharper disable once CheckNamespace
////namespace OLT.Core
////{
////    public static class OltQueryableExtensions
////    {
////        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> queryable, IOltSearcher<TSource> searcher)
////            where TSource : class, IOltEntity
////        {
////            return searcher.BuildQueryable(queryable);
////        }

////        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> queryable, IEnumerable<IOltSearcher<TSource>> searchers)
////            where TSource : class, IOltEntity
////        {
////            var list = searchers.ToList();
////            list.ForEach(searcher => { queryable = queryable.Where(searcher); });
////            return queryable;
////        }

////        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, IOltSortParams sortParams)
////        {
////            return source.OrderByPropertyName(sortParams.PropertyName, sortParams.IsAscending);
////        }

////        public static IOrderedQueryable<T> OrderByPropertyName<T>(this IQueryable<T> source, string memberPath, bool isAscending)
////        {
////            var parameter = Expression.Parameter(typeof(T), "item");
////            var member = memberPath.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
////            var keySelector = Expression.Lambda(member, parameter);
////            var methodCall = Expression.Call(typeof(Queryable), isAscending ? "OrderBy" : "OrderByDescending", new[] { parameter.Type, member.Type }, source.Expression, Expression.Quote(keySelector));
////            return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
////        }

////    }
////}
using System.Linq.Expressions;


// ReSharper disable once CheckNamespace
namespace System.Linq
{
    public static class OltQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderByPropertyName<T>(this IQueryable<T> source, string memberPath, bool isAscending)
        {
            var parameter = Expression.Parameter(typeof(T), "item");
            var member = memberPath.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
            var keySelector = Expression.Lambda(member, parameter);
            var methodCall = Expression.Call(typeof(Queryable), isAscending ? "OrderBy" : "OrderByDescending", new[] { parameter.Type, member.Type }, source.Expression, Expression.Quote(keySelector));
            return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
        }
    }
}
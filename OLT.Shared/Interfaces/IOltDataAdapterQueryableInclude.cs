using System.Linq;

namespace OLT.Core
{
    public interface IOltDataAdapterQueryableInclude<TSource>
        where TSource : class, IOltEntity
    {
        IQueryable<TSource> Include(IQueryable<TSource> queryable);
    }
}
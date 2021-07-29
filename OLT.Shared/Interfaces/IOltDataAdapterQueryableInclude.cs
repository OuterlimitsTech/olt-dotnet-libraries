using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterQueryableInclude<TSource>
    {
        IQueryable<TSource> Include(IQueryable<TSource> queryable);
    }
}
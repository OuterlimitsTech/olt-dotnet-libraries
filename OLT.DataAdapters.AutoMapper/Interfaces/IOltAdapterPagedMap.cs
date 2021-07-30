using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterPagedMap2<TEntity> : IOltAdapterMap2
    {
        IQueryable<TEntity> DefaultOrderBy(IQueryable<TEntity> queryable);
    }
}
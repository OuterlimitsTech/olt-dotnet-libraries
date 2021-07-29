using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterPagedMap<TEntity> : IOltAdapterMap
        where TEntity : class, IOltEntity
    {
        IQueryable<TEntity> DefaultOrderBy(IQueryable<TEntity> queryable);
    }
}
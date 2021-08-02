using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterPagedMap<TEntity, TDestination> : IOltAdapterMap<TEntity, TDestination>, IOltAdapter
    {
        IQueryable<TEntity> DefaultOrderBy(IQueryable<TEntity> queryable);
    }
}
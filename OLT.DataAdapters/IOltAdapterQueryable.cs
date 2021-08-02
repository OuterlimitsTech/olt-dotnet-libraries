using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterQueryable<in TEntity, out TDestination> : IOltAdapter
    {
        IQueryable<TDestination> Map(IQueryable<TEntity> queryable);
    }
}
using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterQueryable<in TEntity, out TModel> : IOltAdapter
    {
        IQueryable<TModel> Map(IQueryable<TEntity> queryable);
    }
}
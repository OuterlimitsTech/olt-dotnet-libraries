using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterQueryable<in TEntity, out TModel> : IOltAdapter
        where TEntity : IOltEntity
    {
        IQueryable<TModel> Map(IQueryable<TEntity> queryable);
    }
}
using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterPagedMap<TEntity, TModel> : IOltAdapterMap<TEntity, TModel>, IOltAdapter
    {
        IQueryable<TEntity> DefaultOrderBy(IQueryable<TEntity> queryable);
    }
}
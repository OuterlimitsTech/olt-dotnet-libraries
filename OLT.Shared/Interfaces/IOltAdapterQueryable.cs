using System.Linq;

namespace OLT.Core
{
    //TODO: Move to DataAdapter package
    public interface IOltAdapterQueryable<in TEntity, out TModel> : IOltAdapter
    {
        IQueryable<TModel> Map(IQueryable<TEntity> queryable);
    }
}
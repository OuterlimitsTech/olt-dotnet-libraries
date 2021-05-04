using System.Linq;

namespace OLT.Core
{
    public interface IOltSearcher<TEntity>
        where TEntity : class, IOltEntity
    {
        bool IncludeDeleted { get; }
        IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable);
    }
}
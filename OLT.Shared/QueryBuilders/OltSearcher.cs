using System.Linq;

namespace OLT.Core
{
    public abstract class OltSearcher<TEntity> : IOltSearcher<TEntity>
        where TEntity : class, IOltEntity
    {
        public virtual bool IncludeDeleted => false;
        public abstract IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable);
    }

}
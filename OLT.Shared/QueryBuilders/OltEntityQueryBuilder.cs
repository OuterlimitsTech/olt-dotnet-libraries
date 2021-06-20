using System.Linq;

namespace OLT.Core
{

    public abstract class OltEntityQueryBuilder<TEntity, TValueType> : IOltEntityQueryBuilder<TEntity>
        where TEntity : class, IOltEntity
    {
        public virtual TValueType Value { get; set; }
        public abstract IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable);
    }
}
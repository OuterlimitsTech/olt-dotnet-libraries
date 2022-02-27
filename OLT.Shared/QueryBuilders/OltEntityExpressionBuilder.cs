using System.Linq;

namespace OLT.Core
{

    public abstract class OltEntityExpressionBuilder<TEntity, TValueType> : IOltEntityQueryBuilder<TEntity>
        where TEntity : class, IOltEntity
    {
        public virtual TValueType Value { get; set; }
        public abstract IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable);
    }
}
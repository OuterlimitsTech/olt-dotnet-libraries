using System.Linq;

namespace OLT.Core
{
    public interface IOltEntityQueryBuilder<TEntity>
        where TEntity : class, IOltEntity
    {
        IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable);
    }


    public interface IOltEntityQueryBuilder<TEntity, TValueType> : IOltEntityQueryBuilder<TEntity>
        where TEntity : class, IOltEntity
    {
        TValueType Value { get; set; }
    }
}
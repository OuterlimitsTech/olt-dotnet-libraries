using System.Linq;

namespace OLT.Core
{
    public class OltSearcherGetAll<TEntity> : OltSearcher<TEntity>
        where TEntity : class, IOltEntity
    {

        public OltSearcherGetAll(bool includeDeleted = false)
        {
            IncludeDeleted = includeDeleted;
        }

        public override bool IncludeDeleted { get; }

        public override IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable)
        {
            return queryable;
        }
    }

}
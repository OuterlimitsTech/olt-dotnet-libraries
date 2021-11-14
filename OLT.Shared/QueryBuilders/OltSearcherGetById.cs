using System.Linq;

namespace OLT.Core
{
    public class OltSearcherGetById<TEntity> : OltSearcher<TEntity>
        where TEntity : class, IOltEntityId
    {

        public OltSearcherGetById(int id, bool includeDeleted = true)
        {
            Id = id;
            IncludeDeleted = includeDeleted;
        }

        public int Id { get; }
        public override bool IncludeDeleted { get; }

        public override IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable)
        {
            return queryable.Where(p => p.Id == Id);
        }
    }
}
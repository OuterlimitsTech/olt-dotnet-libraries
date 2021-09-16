using System.Linq;

namespace OLT.Core
{
    public class OltSearcherGetById<TEntity> : OltSearcher<TEntity>
        where TEntity : class, IOltEntityId
    {

        public OltSearcherGetById(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public override bool IncludeDeleted => true;

        public override IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable)
        {
            return queryable.Where(p => p.Id == Id);
        }
    }
}
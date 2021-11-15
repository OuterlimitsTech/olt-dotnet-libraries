using System;
using System.Linq;

namespace OLT.Core
{
    public class OltSearcherGetByUid<TEntity> : OltSearcher<TEntity>
        where TEntity : class, IOltEntityUniqueId
    {

        public OltSearcherGetByUid(Guid uid, bool includeDeleted = true)
        {
            Uid = uid;
            IncludeDeleted = includeDeleted;
        }

        public Guid Uid { get; }
        public override bool IncludeDeleted { get; }

        public override IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable)
        {
            return queryable.Where(p => p.UniqueId == Uid);
        }
    }
}
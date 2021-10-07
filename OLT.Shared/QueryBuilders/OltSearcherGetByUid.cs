using System;
using System.Linq;

namespace OLT.Core
{
    public class OltSearcherGetByUid<TEntity> : OltSearcher<TEntity>
        where TEntity : class, IOltEntityUniqueId
    {

        public OltSearcherGetByUid(Guid uid)
        {
            Uid = uid;
        }

        public Guid Uid { get; }

        public override IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable)
        {
            return queryable.Where(p => p.UniqueId == Uid);
        }
    }
}
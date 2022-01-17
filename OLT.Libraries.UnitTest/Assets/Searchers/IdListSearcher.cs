using System.Collections.Generic;
using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Searchers
{
    public class IdListSearcher<TEntity> : OltSearcher<TEntity>
        where TEntity : class, IOltEntityId        
    {
        public IdListSearcher(params int[] ids)
        {
            Ids = ids.ToList();
        }

        public List<int> Ids { get; }

        public override IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable)
        {
            return queryable.Where(p => Ids.Contains(p.Id));
        }
    }
}
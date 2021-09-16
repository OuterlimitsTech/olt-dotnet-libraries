using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Searchers
{
    public class PersonLastNameStartsWithSearcher : OltSearcher<PersonEntity>
    {
        public PersonLastNameStartsWithSearcher(string startsWith)
        {
            StartsWith = startsWith;
        }

        public string StartsWith { get; }

        public override IQueryable<PersonEntity> BuildQueryable(IQueryable<PersonEntity> queryable)
        {
            return queryable.Where(p => p.NameLast.Contains(StartsWith));
        }
    }
}
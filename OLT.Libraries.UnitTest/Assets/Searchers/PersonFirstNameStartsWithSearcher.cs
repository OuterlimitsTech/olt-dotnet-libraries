using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Searchers
{
    public class PersonFirstNameStartsWithSearcher : OltSearcher<PersonEntity>
    {
        public PersonFirstNameStartsWithSearcher(string startsWith)
        {
            StartsWith = startsWith;
        }

        public string StartsWith { get; }

        public override IQueryable<PersonEntity> BuildQueryable(IQueryable<PersonEntity> queryable)
        {
            return queryable.Where(p => p.NameFirst.Contains(StartsWith));
        }
    }
}
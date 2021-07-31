using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity.Models;
using OLT.Libraries.UnitTest.Assests.Models;

namespace OLT.Libraries.UnitTest.Assests.Adapters
{
    // ReSharper disable once InconsistentNaming
    public class PersonDtoAdapter : OltAdapterPaged<PersonEntity, PersonDto>
    {
        public override void Map(PersonEntity source, PersonDto destination)
        {
            destination.PersonId = source.Id;
            destination.First = source.NameFirst;
            destination.Middle = source.NameMiddle;
            destination.Last = source.NameLast;
        }

        public override void Map(PersonDto source, PersonEntity destination)
        {
            destination.NameFirst = source.First;
            destination.NameMiddle = source.Middle;
            destination.NameLast = source.Last;
        }

        public override IQueryable<PersonDto> Map(IQueryable<PersonEntity> queryable)
        {
            return queryable.Select(entity => new PersonDto
            {
                PersonId = entity.Id,
                First = entity.NameFirst,
                Middle = entity.NameMiddle,
                Last = entity.NameLast
            });
        }

        public override IQueryable<PersonEntity> DefaultOrderBy(IQueryable<PersonEntity> queryable)
        {
            return queryable.OrderBy(p => p.NameLast).ThenBy(p => p.NameFirst).ThenBy(p => p.Id);
        }
    }
}
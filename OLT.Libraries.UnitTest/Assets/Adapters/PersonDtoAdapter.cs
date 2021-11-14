using System.Linq;
using Microsoft.EntityFrameworkCore;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Adapters
{
    // ReSharper disable once InconsistentNaming
    public class PersonDtoAdapter : OltAdapterPaged<PersonEntity, PersonDto>, IOltAdapterQueryableInclude<PersonEntity>
    {
        public override void Map(PersonEntity source, PersonDto destination)
        {
            destination.PersonId = source.Id;
            destination.UniqueId = source.UniqueId;
            destination.First = source.NameFirst;
            destination.Middle = source.NameMiddle;
            destination.Last = source.NameLast;
        }

        public override void Map(PersonDto source, PersonEntity destination)
        {
            destination.NameFirst = source.First;            
            destination.NameMiddle = source.Middle;
            destination.NameLast = source.Last;

            if (source.UniqueId.HasValue)
            {
                destination.UniqueId = source.UniqueId.Value;
            }
        }

        public override IQueryable<PersonDto> Map(IQueryable<PersonEntity> queryable)
        {
            return queryable.Select(entity => new PersonDto
            {
                PersonId = entity.Id,
                UniqueId = entity.UniqueId,
                First = entity.NameFirst,
                Middle = entity.NameMiddle,
                Last = entity.NameLast
            });
        }

        public override IQueryable<PersonEntity> DefaultOrderBy(IQueryable<PersonEntity> queryable)
        {
            return queryable.OrderBy(p => p.NameLast).ThenBy(p => p.NameFirst).ThenBy(p => p.Id);
        }

        public IQueryable<PersonEntity> Include(IQueryable<PersonEntity> queryable)
        {
            return queryable.Include(i => i.PersonType);
        }
    }
}
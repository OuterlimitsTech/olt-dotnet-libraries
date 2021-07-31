using System.Linq;
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity.Models;
using OLT.Libraries.UnitTest.Assests.Models;

namespace OLT.Libraries.UnitTest.Assests.Adapters
{
    // ReSharper disable once InconsistentNaming
    public class PersonAutoMapperDtoAdapter : OltAdapterPagedMap<PersonEntity, PersonAutoMapperDto>
    {
        public override void BuildMap(IMappingExpression<PersonEntity, PersonAutoMapperDto> mappingExpression)
        {
            PersonAutoMapperDto.BuildMap(mappingExpression);
        }

        public override IQueryable<PersonEntity> DefaultOrderBy(IQueryable<PersonEntity> queryable)
        {
            return queryable.OrderBy(p => p.NameLast).ThenBy(p => p.NameFirst).ThenBy(p => p.Id);
        }
    }
}
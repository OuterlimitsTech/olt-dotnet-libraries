using System.Linq;
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Adapters
{
    // ReSharper disable once InconsistentNaming
    public class PersonAutoMapperPaged : OltAdapterPagedMap<PersonEntity, PersonAutoMapperDto>
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
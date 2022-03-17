////using System.Linq;
////using AutoMapper;
////using OLT.Core;
////using OLT.Libraries.UnitTest.Assets.Entity.Models;
////using OLT.Libraries.UnitTest.Assets.Models;

////namespace OLT.Libraries.UnitTest.Assets.Adapters
////{
////    // ReSharper disable once InconsistentNaming
////    public class PersonAutoMapperPagedMap : OltAdapterPagedMap<PersonEntity, PersonAutoMapperPagedDto>
////    {
////        public override void BuildMap(IMappingExpression<PersonEntity, PersonAutoMapperPagedDto> mappingExpression)
////        {
////            PersonAutoMapperPagedDto.BuildMap(mappingExpression);
////        }

////        public override IQueryable<PersonEntity> DefaultOrderBy(IQueryable<PersonEntity> queryable)
////        {
////            return queryable.OrderBy(p => p.NameLast).ThenBy(p => p.NameFirst).ThenBy(p => p.Id);
////        }
////    }
////}
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Maps
{
    // ReSharper disable once InconsistentNaming
    public class PersonTestModelAdapterMap : OltAdapterMap<PersonEntity, PersonAutoMapperModel>
    {
        public override void BuildMap(IMappingExpression<PersonEntity, PersonAutoMapperModel> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.PersonId, opt => opt.MapFrom(t => t.Id))
                .ForMember(f => f.Name, opt => opt.MapFrom(t => t));


            mappingExpression.ReverseMap();
        }
    }
}
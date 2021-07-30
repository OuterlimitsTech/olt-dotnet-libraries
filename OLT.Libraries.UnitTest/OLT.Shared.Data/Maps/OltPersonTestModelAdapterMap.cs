using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Models;
using OLT.Libraries.UnitTest.Models.Entity;

namespace OLT.Libraries.UnitTest.OLT.Shared.Data.Maps
{
    public class OltPersonTestModelAdapterMap : OltAdapterMap<OltPersonEntity, OltPersonTestModel>
    {
        public override void BuildMap(IMappingExpression<OltPersonEntity, OltPersonTestModel> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.PersonId, opt => opt.MapFrom(t => t.Id))
                .ForMember(f => f.Name, opt => opt.MapFrom(t => t));


            mappingExpression.ReverseMap();
        }
    }
}
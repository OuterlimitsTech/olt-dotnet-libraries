using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Models;
using OLT.Libraries.UnitTest.Models.Entity;

namespace OLT.Libraries.UnitTest.OLT.Shared.Data.Maps
{
    public class OltNameTestModelMaps : OltAdapterMap, IOltAdapterMap2<OltPersonEntity, OltNameTestModel>, IOltAdapterMap2<OltUserEntity, OltNameTestModel>
    {

        public override void CreateMaps()
        {
            BuildMap(CreateMap<OltPersonEntity, OltNameTestModel>());
            BuildMap(CreateMap<OltUserEntity, OltNameTestModel>());
        }

        public void BuildMap(IMappingExpression<OltPersonEntity, OltNameTestModel> mappingExpression)
        {
            OltNameTestModel.BuildMap(mappingExpression);
        }

        public void BuildMap(IMappingExpression<OltUserEntity, OltNameTestModel> mappingExpression)
        {
            OltNameTestModel.BuildMap(mappingExpression);
        }
    }
}
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity.Models;
using OLT.Libraries.UnitTest.Assests.Models;

namespace OLT.Libraries.UnitTest.Assests.Maps
{
    public class OltNameTestModelMaps : OltAdapterMap, IOltAdapterMap2<PersonEntity, OltNameTestModel>, IOltAdapterMap2<UserEntity, OltNameTestModel>
    {

        public override void CreateMaps()
        {
            BuildMap(CreateMap<PersonEntity, OltNameTestModel>());
            BuildMap(CreateMap<UserEntity, OltNameTestModel>());
        }

        public void BuildMap(IMappingExpression<PersonEntity, OltNameTestModel> mappingExpression)
        {
            OltNameTestModel.BuildMap(mappingExpression);
        }

        public void BuildMap(IMappingExpression<UserEntity, OltNameTestModel> mappingExpression)
        {
            OltNameTestModel.BuildMap(mappingExpression);
        }
    }
}
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Maps
{
    // ReSharper disable once InconsistentNaming
    public class NameModelMaps : OltAdapterMap, IOltAdapterMap<PersonEntity, NameAutoMapperModel>, IOltAdapterMap<UserEntity, NameAutoMapperModel>
    {

        public override void CreateMaps()
        {
            BuildMap(CreateMap<PersonEntity, NameAutoMapperModel>());
            BuildMap(CreateMap<UserEntity, NameAutoMapperModel>());
        }

        public void BuildMap(IMappingExpression<PersonEntity, NameAutoMapperModel> mappingExpression)
        {
            NameAutoMapperModel.BuildMap(mappingExpression);
        }

        public void BuildMap(IMappingExpression<UserEntity, NameAutoMapperModel> mappingExpression)
        {
            NameAutoMapperModel.BuildMap(mappingExpression);
        }
    }
}
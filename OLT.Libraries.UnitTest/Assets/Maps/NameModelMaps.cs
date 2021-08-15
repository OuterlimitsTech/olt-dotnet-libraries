using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Maps
{
    // ReSharper disable once InconsistentNaming
    public class NameModelMaps : Profile
    {
        public NameModelMaps()
        {
            NameAutoMapperModel.BuildMap(CreateMap<PersonEntity, NameAutoMapperModel>());
            NameAutoMapperModel.BuildMap(CreateMap<UserEntity, NameAutoMapperModel>());
        }     
    }
}
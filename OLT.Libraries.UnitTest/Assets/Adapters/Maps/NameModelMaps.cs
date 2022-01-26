using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;
using System.Linq;

namespace OLT.Libraries.UnitTest.Assets.Maps
{
    // ReSharper disable once InconsistentNaming
    public class NameModelMaps : Profile
    {
        public NameModelMaps()
        {
            NameAutoMapperModel.BuildMap(CreateMap<PersonEntity, NameAutoMapperModel>()).AfterMap(p => p.OrderBy(o => o.Last).ThenBy(o => o.First));
            NameAutoMapperModel.BuildMap(CreateMap<UserEntity, NameAutoMapperModel>()).AfterMap(new NameAutoMapperModelAfterMap());
        }     
    }


    public class NameAutoMapperModelAfterMap : OltAdapterAfterMap<UserEntity, NameAutoMapperModel>
    {
        public override IQueryable<NameAutoMapperModel> AfterMap(IQueryable<NameAutoMapperModel> queryable)
        {
            return queryable.OrderBy(p => p.Last).ThenBy(p => p.First);
        }
    }
}
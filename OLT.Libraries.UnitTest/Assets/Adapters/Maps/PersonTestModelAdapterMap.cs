using System.Linq;
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Maps
{
    // ReSharper disable once InconsistentNaming
    public class PersonTestModelAdapterMaps : Profile 
    {
        
        public PersonTestModelAdapterMaps()
        {
            BuildMap(CreateMap<PersonEntity, PersonAutoMapperModel>()); 
            BuildMap(CreateMap<PersonEntity, PersonAddressModel>());
        }
        public void BuildMap(IMappingExpression<PersonEntity, PersonAutoMapperModel> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.PersonId, opt => opt.MapFrom(t => t.Id))
                .ForMember(f => f.UniqueId, opt => opt.MapFrom(t => t.UniqueId))
                .ForMember(f => f.Name, opt => opt.MapFrom(t => t));


            mappingExpression.ReverseMap();
        }

        public void BuildMap(IMappingExpression<PersonEntity, PersonAddressModel> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.PersonId, opt => opt.MapFrom(t => t.Id))
                .ForMember(f => f.UniqueId, opt => opt.MapFrom(t => t.UniqueId))
                .ForMember(f => f.Name, opt => opt.MapFrom(t => t))
                .ForMember(f => f.Street1, opt => opt.MapFrom(t => t.Addresses.SelectMany(s => s.Street).ToList()))  //This is invalid
                .ReverseMap()
                .ForMember(f => f.Addresses, opt => opt.MapFrom(t => t.Created))
                ;
        }
    }
}
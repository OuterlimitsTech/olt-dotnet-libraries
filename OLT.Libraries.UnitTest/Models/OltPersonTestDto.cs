using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Models.Entity;

namespace OLT.Libraries.UnitTest.Models
{
    public class OltPersonTestDto : OltPersonName
    {
        public int? PersonId { get; set; }

        public static void BuildMap(IMappingExpression<OltPersonEntity, OltPersonTestDto> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.PersonId, opt => opt.MapFrom(t => t.Id))
                .ForMember(f => f.First, opt => opt.MapFrom(t => t.NameFirst))
                .ForMember(f => f.Middle, opt => opt.MapFrom(t => t.NameMiddle))
                .ForMember(f => f.Last, opt => opt.MapFrom(t => t.NameLast))
                .ReverseMap()
                .ForMember(f => f.Id, opt => opt.Ignore())
                ;
        }
    }
}
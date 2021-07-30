using System;
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Models.Entity;

namespace OLT.Libraries.UnitTest.Models
{

    public class OltNameTestModel : OltPersonName
    {

        
        public static void BuildMap(IMappingExpression<OltPersonEntity, OltNameTestModel> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.First, opt => opt.MapFrom(t => t.NameFirst))
                .ForMember(f => f.Middle, opt => opt.MapFrom(t => t.NameMiddle))
                .ForMember(f => f.Last, opt => opt.MapFrom(t => t.NameLast))
                .ReverseMap();
        }

        public static void BuildMap(IMappingExpression<OltUserEntity, OltNameTestModel> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.First, opt => opt.MapFrom(t => t.FirstName))
                .ForMember(f => f.Middle, opt => opt.MapFrom(t => t.MiddleName))
                .ForMember(f => f.Last, opt => opt.MapFrom(t => t.LastName))
                .ForMember(f => f.Suffix, opt => opt.MapFrom(t => t.NameSuffix))
                .ReverseMap();
        }

      
    }
}
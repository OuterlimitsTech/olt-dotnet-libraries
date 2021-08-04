using System;
using AutoMapper;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.Models
{
    // ReSharper disable once InconsistentNaming
    public class PersonAutoMapperDto : PersonDto
    {
        public DateTimeOffset Created { get; set; }

        public static void BuildMap(IMappingExpression<PersonEntity, PersonAutoMapperDto> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.PersonId, opt => opt.MapFrom(t => t.Id))
                .ForMember(f => f.First, opt => opt.MapFrom(t => t.NameFirst))
                .ForMember(f => f.Middle, opt => opt.MapFrom(t => t.NameMiddle))
                .ForMember(f => f.Last, opt => opt.MapFrom(t => t.NameLast))
                .ForMember(f => f.Created, opt => opt.MapFrom(t => t.CreateDate))
                .ReverseMap()
                .ForMember(f => f.Id, opt => opt.Ignore())
                .ForMember(f => f.CreateDate, opt => opt.Ignore())
                ;
        }
    }
}
using System.Linq;
using AutoMapper;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Adapters
{
    public class PersonAddressModelPagedMap : OltAdapterPagedMap<PersonEntity, PersonAddressInvalidPagedModel>
    {
        public override void BuildMap(IMappingExpression<PersonEntity, PersonAddressInvalidPagedModel> mappingExpression)
        {
            mappingExpression
                .ForMember(f => f.PersonId, opt => opt.MapFrom(t => t.Id))
                .ForMember(f => f.Name, opt => opt.MapFrom(t => t))
                .ForMember(f => f.Street1, opt => opt.MapFrom(t => t.Addresses.SelectMany(s => s.Street).ToList()))  //This is invalid
                .ReverseMap()
                .ForMember(f => f.Addresses, opt => opt.MapFrom(t => t.Created))
                ;
        }

        public override IQueryable<PersonEntity> DefaultOrderBy(IQueryable<PersonEntity> queryable)
        {
            return queryable.OrderBy(p => p.NameLast).ThenBy(p => p.NameFirst).ThenBy(p => p.Id);
        }
    }
}
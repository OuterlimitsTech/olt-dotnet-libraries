using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Models;
using OLT.Libraries.UnitTest.Models.Entity;

namespace OLT.Libraries.UnitTest.OLT.Shared.Data.Adapters
{

    public class OltUserTestModelAdapter : OltAdapterPaged<OltUserEntity, OltUserTestModel>
    {
        public override void Map(OltUserEntity source, OltUserTestModel destination)
        {
            destination.UserGuid = source.UniqueId;
            destination.Name.First = source.FirstName;
            destination.Name.Middle = source.MiddleName;
            destination.Name.Last = source.LastName;
            destination.Name.Suffix = source.NameSuffix;
        }

        public override void Map(OltUserTestModel source, OltUserEntity destination)
        {
            destination.UniqueId = source.UserGuid;
            destination.FirstName = source.Name.First;
            destination.MiddleName = source.Name.Middle;
            destination.LastName = source.Name.Last;
            destination.NameSuffix = source.Name.Suffix;
        }

        public override IQueryable<OltUserTestModel> Map(IQueryable<OltUserEntity> queryable)
        {
            return queryable.Select(entity => new OltUserTestModel
            {
                UserId = entity.Id,
                UserGuid = entity.UniqueId,
                Name = new OltNameTestModel
                {
                    First = entity.FirstName,
                    Middle = entity.MiddleName,
                    Last = entity.LastName,
                    Suffix = entity.NameSuffix
                }
            });
        }

        public override IQueryable<OltUserEntity> DefaultOrderBy(IQueryable<OltUserEntity> queryable)
        {
            return queryable.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ThenBy(p => p.Id);
        }
    }
}
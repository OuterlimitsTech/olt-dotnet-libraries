using System.Linq;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Adapters
{
    public class UserTempDtoAdapter : OltAdapterQueryable<UserEntity, UserTempDto>
    {
        public override void Map(UserEntity source, UserTempDto destination)
        {
            destination.UserGuid = source.UniqueId;
            destination.Name.First = source.FirstName;
            destination.Name.Middle = source.MiddleName;
            destination.Name.Last = source.LastName;
            destination.Name.Suffix = source.NameSuffix;
        }

        public override void Map(UserTempDto source, UserEntity destination)
        {
            destination.UniqueId = source.UserGuid;
            destination.FirstName = source.Name.First;
            destination.MiddleName = source.Name.Middle;
            destination.LastName = source.Name.Last;
            destination.NameSuffix = source.Name.Suffix;
        }

        public override IQueryable<UserTempDto> Map(IQueryable<UserEntity> queryable)
        {
            return queryable.Select(entity => new UserTempDto
            {
                UserId = entity.Id,
                UserGuid = entity.UniqueId,
                Name = new NameAutoMapperModel
                {
                    First = entity.FirstName,
                    Middle = entity.MiddleName,
                    Last = entity.LastName,
                    Suffix = entity.NameSuffix
                }
            });
        }


    }
}
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.Adapters
{
    public class UserDtoAdapter : OltAdapter<UserEntity, UserDto>
    {
        public override void Map(UserEntity source, UserDto destination)
        {
            destination.UserId = source.Id;
            destination.UserGuid = source.UniqueId;
            destination.First = source.FirstName;
            destination.Middle = source.MiddleName;
            destination.Last = source.LastName;
            destination.Suffix = source.NameSuffix;

        }

        public override void Map(UserDto source, UserEntity destination)
        {
            destination.UniqueId = source.UserGuid;
            destination.FirstName = source.First;
            destination.MiddleName = source.Middle;
            destination.LastName = source.Last;
            destination.NameSuffix = source.Suffix;
        }

    }
}
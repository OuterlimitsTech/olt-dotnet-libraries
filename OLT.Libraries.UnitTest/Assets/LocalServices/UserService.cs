using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    public class UserService : BaseDbEntityIdService<UserEntity>, IUserService
    {
        public UserService(
            IOltServiceManager serviceManager,
            SqlDatabaseContext context) : base(serviceManager, context)
        {
        }
    }
}
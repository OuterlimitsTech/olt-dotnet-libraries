using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;
using System.Linq;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    public interface IUserService : IOltEntityService<UserEntity>
    {
        IQueryable<UserEntity> GetRepository();
    }
}
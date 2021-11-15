using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    public interface IPersonService : IOltEntityIdService<PersonEntity>
    {
    }

    public interface IPersonUniqueIdService : IOltEntityUniqueIdService<PersonEntity>
    {
    }

    public interface IPersonService2 : IOltEntityIdService<PersonEntity>
    {
    }
}
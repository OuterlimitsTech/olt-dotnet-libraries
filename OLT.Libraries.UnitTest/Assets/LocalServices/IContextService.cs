using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Models;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    public interface IContextService : IOltCoreService
    {
        PersonAutoMapperModel Get();
    }
}
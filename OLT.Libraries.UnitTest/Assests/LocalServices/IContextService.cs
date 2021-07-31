using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Models;

namespace OLT.Libraries.UnitTest.Assests.LocalServices
{
    public interface IContextService : IOltCoreService
    {
        PersonAutoMapperModel Get();
    }
}
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    // ReSharper disable once InconsistentNaming
    public abstract class BaseContextService : OltContextService<SqlDatabaseContext>
    {
        protected BaseContextService(
            IOltServiceManager serviceManager,
            SqlDatabaseContext context) : base(serviceManager, context)
        {
        }
    }
}
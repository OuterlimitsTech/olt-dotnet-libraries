using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    // ReSharper disable once InconsistentNaming
    public abstract class BaseEntityService : OltContextService<SqlDatabaseContext>
    {
        protected BaseEntityService(
            IOltServiceManager serviceManager, 
            SqlDatabaseContext context) : base(serviceManager, context)
        {
        }
    }
}
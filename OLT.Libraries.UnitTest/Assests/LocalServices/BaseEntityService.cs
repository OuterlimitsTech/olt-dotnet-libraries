using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity;

namespace OLT.Libraries.UnitTest.Assests.LocalServices
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
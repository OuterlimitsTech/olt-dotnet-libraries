using OLT.Core;
using OLT.Libraries.UnitTest.Assests.Entity;

namespace OLT.Libraries.UnitTest.Assests.LocalServices
{
    // ReSharper disable once InconsistentNaming
    public abstract class BaseDbEntityIdService<TEntity> : OltEntityIdService<SqlDatabaseContext, TEntity>
        where TEntity : class, IOltEntity, IOltEntityId, new()
    {
        protected BaseDbEntityIdService(
            IOltServiceManager serviceManager,
            SqlDatabaseContext context) : base(serviceManager, context)
        {
        }
    }
}
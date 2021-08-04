using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
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
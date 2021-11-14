using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    // ReSharper disable once InconsistentNaming
    public abstract class BaseDbEntityUniqueIdService<TEntity> : OltEntityUniqueIdService<SqlDatabaseContext, TEntity>
        where TEntity : class, IOltEntity, IOltEntityUniqueId, new()
    {
        protected BaseDbEntityUniqueIdService(
            IOltServiceManager serviceManager,
            SqlDatabaseContext context) : base(serviceManager, context)
        {
        }
    }
}
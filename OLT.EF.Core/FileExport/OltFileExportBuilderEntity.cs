using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public abstract class OltFileExportBuilderEntity<TContext, TEntity> : OltDisposable, IOltFileExportBuilderEntity<TContext, TEntity>
        where TEntity : class, IOltEntity
        where TContext : DbContext, IOltDbContext
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TContext context, IQueryable<TEntity> queryable);
    }
}
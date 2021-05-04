using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public interface IOltFileExportBuilderEntity<in TContext, in TEntity> : IOltFileExportBuilder
        where TContext : DbContext, IOltDbContext
        where TEntity : class, IOltEntity
    {
        IOltFileBase64 Build(TContext context, IQueryable<TEntity> queryable);
    }
}
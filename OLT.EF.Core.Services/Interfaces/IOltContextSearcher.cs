using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public interface IOltContextSearcher<in TContext, TEntity> : IOltSearcher<TEntity>
        where TEntity : class, IOltEntity
        where TContext : DbContext, IOltDbContext
    {
        IQueryable<TEntity> BuildQueryable(TContext context, IQueryable<TEntity> queryable);
    }

}
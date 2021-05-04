using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OLT.Core
{
    public interface IOltDbContext : IOltDbContext<DatabaseFacade>
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }


}
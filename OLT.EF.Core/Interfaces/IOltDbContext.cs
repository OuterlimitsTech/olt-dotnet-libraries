using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OLT.Core
{
    public interface IOltDbContext : IDisposable //IOltDbContext<DatabaseFacade>
    {
        string DefaultAnonymousUser { get; }
        string AuditUser { get; }
        bool ApplyGlobalDeleteFilter { get; }
        DatabaseFacade Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }


}
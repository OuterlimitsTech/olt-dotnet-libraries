using System;
using System.Threading;
using System.Threading.Tasks;

namespace OLT.Core
{
    public interface IOltDbContext<out TDatabase> : IDisposable where TDatabase : class
    {
        string DefaultAnonymousUser { get; }
        string AuditUser { get; }
        TDatabase Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
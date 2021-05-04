using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace OLT.Core
{
    public interface IOltDeletingRecord
    {
        void DeletingRecord(IOltDbContext db, EntityEntry entityEntry);
    }
}
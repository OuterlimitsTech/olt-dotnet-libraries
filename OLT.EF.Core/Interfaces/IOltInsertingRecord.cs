using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace OLT.Core
{
    public interface IOltInsertingRecord
    {
        void InsertingRecord(IOltDbContext db, EntityEntry entityEntry);
    }
}
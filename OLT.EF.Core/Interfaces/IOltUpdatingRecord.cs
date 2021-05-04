using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace OLT.Core
{
    public interface IOltUpdatingRecord
    {
        void UpdatingRecord(IOltDbContext db, EntityEntry entityEntry);
    }
}
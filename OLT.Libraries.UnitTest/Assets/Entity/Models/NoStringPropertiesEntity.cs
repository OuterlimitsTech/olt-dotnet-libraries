using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    [Table("NoStringPropertiesEntity")]
    public class NoStringPropertiesEntity : IOltEntityId, IOltUpdatingRecord
    {
        public int Id { get; set; }

        public int Value1 { get; set; }
        public int Value2 { get; set; }

        public void UpdatingRecord(IOltDbContext db, EntityEntry entityEntry)
        {
            throw new System.Exception("This is for testing Context Exception Handling");
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Enums;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    // ReSharper disable once InconsistentNaming
    public class UserEntity : OltEntityId, IOltEntityUniqueId, IOltInsertingRecord
    {
        public Guid UniqueId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string NameSuffix { get; set; }

        public int StatusId { get; set; }
        public virtual StatusTypeCodeEntity Status { get; set; }

        public void InsertingRecord(IOltDbContext db, EntityEntry entityEntry)
        {
            StatusId = (int)StatusTypes.Active;
        }

        public static UserEntity FakerEntity(bool emptyGuid = false)
        {
            return new UserEntity
            {
                UniqueId = emptyGuid ? Guid.Empty :Guid.NewGuid(),
                FirstName = Faker.Name.First(),
                MiddleName = Faker.Name.Middle(),
                LastName = Faker.Name.Last()
            };
        }

  
    }
}
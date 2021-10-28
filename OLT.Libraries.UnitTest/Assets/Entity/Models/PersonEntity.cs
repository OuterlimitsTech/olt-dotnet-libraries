using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OLT.Core;
using OLT.Libraries.UnitTest.Assets.Entity.Models.GeneralCode;
using OLT.Libraries.UnitTest.Assets.Enums;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    // ReSharper disable once InconsistentNaming
    [Table("People")]
    public class PersonEntity : OltEntityIdDeletable, IOltInsertingRecord, IOltUpdatingRecord, IOltDeletingRecord, IOltEntityUniqueId
    {
        public Guid UniqueId { get; set; }

        [StringLength(100)]
        public string NameFirst { get; set; }
        [StringLength(100)]
        public string NameMiddle { get; set; }
        [StringLength(100)]
        public string NameLast { get; set; }

        [StringLength(20)]
        public string ActionCode { get; set; }

        public int? PersonTypeId { get; set; }
        public virtual PersonTypeCodeEntity PersonType { get; set; }

        public int? StatusTypeId { get; set; }
        public virtual StatusTypeCodeEntity StatusType { get; set; }

        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        public int? SexId { get; set; }
        public virtual Sex Sex { get; set; }

        public virtual List<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();

        public void InsertingRecord(IOltDbContext db, EntityEntry entityEntry)
        {
            PersonTypeId ??= Faker.RandomNumber.Next(1000, 1002);
            StatusTypeId ??= (int)StatusTypes.Active;
            ActionCode = "Insert";
        }

        public void UpdatingRecord(IOltDbContext db, EntityEntry entityEntry)
        {
            ActionCode = "Update";
        }

        public void DeletingRecord(IOltDbContext db, EntityEntry entityEntry)
        {
            DeletedOn = DateTimeOffset.Now;
            DeletedBy = db.AuditUser;
        }


        public static PersonEntity FakerEntity()
        {
            return new PersonEntity
            {
                NameFirst = Faker.Name.First(),
                NameMiddle = Faker.Name.Middle(),
                NameLast = Faker.Name.Last(),
            };
        }

        
    }
}
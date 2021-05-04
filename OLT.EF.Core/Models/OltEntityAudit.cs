using System;
using System.ComponentModel.DataAnnotations;

namespace OLT.Core
{
    public abstract class OltEntityAudit : IOltEntityAudit
    {
       
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;

        [Required]
        [StringLength(100)]
        public string CreateUser { get; set; }

        public DateTimeOffset? ModifyDate { get; set; }

        [StringLength(100)]
        public string ModifyUser { get; set; }


        //protected bool FieldChanged<T>(DbEntityEntry entityEntry, string fieldName, T defaultValue)
        //{
        //    var orig = entityEntry.OriginalValues[fieldName] == null ? defaultValue : (T)entityEntry.OriginalValues[fieldName];
        //    var current = entityEntry.CurrentValues[fieldName] == null ? defaultValue : (T)entityEntry.CurrentValues[fieldName];
        //    return !orig.Equals(current);
        //}
    }
}

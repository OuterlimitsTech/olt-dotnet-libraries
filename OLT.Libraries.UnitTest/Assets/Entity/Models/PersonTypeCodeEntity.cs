using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    [Table("PersonType")]
    public class PersonTypeCodeEntity : OltEntityIdDeletable, IOltEntityUniqueId, IOltEntityCodeValue
    {
        public Guid UniqueId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public short SortOrder { get; set; }
        
    }
}
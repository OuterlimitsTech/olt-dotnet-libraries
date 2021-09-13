using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    [Table("StatusType")]
    public class StatusTypeCodeEntity : OltEntityIdDeletable, IOltEntityUniqueId, IOltEntityCodeValueEnum
    {
        public Guid UniqueId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public short SortOrder { get; set; }

    }


    [Table("Country")]
    public class CountryCodeEntity : OltEntityIdDeletable, IOltEntityUniqueId, IOltEntityCodeValueAbbrev, IOltEntityCodeValueDescription
    {
        public Guid UniqueId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(5)]
        public string Abbreviation { get; set; }
        
        public string Description { get; set; }

        public short SortOrder { get; set; }

        
    }
}
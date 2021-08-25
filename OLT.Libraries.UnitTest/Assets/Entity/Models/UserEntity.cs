using System;
using System.ComponentModel.DataAnnotations;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Entity.Models
{
    // ReSharper disable once InconsistentNaming
    public class UserEntity : OltEntityId, IOltEntityUniqueId
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
        
    }
}
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

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace OLT.Core
{
    public abstract class OltEntityDeletable : OltEntityAudit, IOltEntityDeletable
    {

        public virtual DateTimeOffset? DeletedOn { get; set; }

        [MaxLength(100)]
        public virtual string DeletedBy { get; set; }
    }
}
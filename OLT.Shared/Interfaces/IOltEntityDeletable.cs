using System;

namespace OLT.Core
{
    public interface IOltEntityDeletable : IOltEntityAudit
    {
        DateTimeOffset? DeletedOn { get; set; }
        string DeletedBy { get; set; }
    }
}
using System;

namespace OLT.Core
{
    public interface IOltEntityUniqueId : IOltEntity
    {
        Guid UniqueId { get; set; }
    }
}
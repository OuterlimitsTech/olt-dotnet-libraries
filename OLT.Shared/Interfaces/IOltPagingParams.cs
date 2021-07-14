using System;

namespace OLT.Core
{
    public interface IOltPaged
    {
        int Page { get; set; }
        int Size { get; set; }
    }

    public interface IOltPagingParams : IOltPaged
    {
        [Obsolete("Move to Page")]
        int PageNumber { get; set; }
        [Obsolete("Move to Size")]
        int PageSize { get; set; }
    }
}
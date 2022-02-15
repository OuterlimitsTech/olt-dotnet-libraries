using System;

namespace OLT.Core
{
    [Obsolete("Removed in 6.x")]
    public interface IOltPagingWithSortParams : IOltPagingParams, IOltSortParams
    {
    }
}
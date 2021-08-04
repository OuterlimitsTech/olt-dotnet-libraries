using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltPaged
    {
        int Page { get; set; }
        int Size { get; set; }
    }

    public interface IOltPaged<TModel> : IOltPaged
    {
        int Count { get; set; }
        IEnumerable<TModel> Data { get; set; }
    }
}
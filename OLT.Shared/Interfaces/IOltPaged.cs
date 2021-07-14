using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltPaged<TModel> : IOltPaged  
        where TModel : class
    {
        int Count { get; set; }
        IEnumerable<TModel> Data { get; set; }
    }
}
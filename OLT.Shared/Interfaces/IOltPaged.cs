using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltPaged<TModel> where TModel : class
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
        //int PageIndex { get; }
        int Count { get; set; }
        IEnumerable<TModel> Data { get; set; }
    }
}
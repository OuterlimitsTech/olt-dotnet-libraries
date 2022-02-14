using Newtonsoft.Json;

namespace OLT.Core
{
    public class OltPagingWithSortParams : OltPagingParams, IOltPagingWithSortParams
    {        
        public virtual string PropertyName { get; set; }        
        public virtual bool IsAscending { get; set; } = true;
    }
}
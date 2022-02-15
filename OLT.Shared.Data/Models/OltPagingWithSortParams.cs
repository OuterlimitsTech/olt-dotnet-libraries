using Newtonsoft.Json;
using System;

namespace OLT.Core
{
    [Obsolete("Removed in 6.x")]
    public class OltPagingWithSortParams : OltPagingParams, IOltPagingWithSortParams
    {
        [JsonProperty("sort")]
        public virtual string PropertyName { get; set; }
        [JsonProperty("asc")]
        public virtual bool IsAscending { get; set; } = true;
    }
}
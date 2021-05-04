using System.Collections.Generic;

namespace OLT.Core
{
    public class OltPagedData<TModel> : IOltPaged<TModel> where TModel : class
    {
        public virtual string SortBy { get; set; }
        public virtual bool Asc { get; set; }
        public virtual int PageSize { get; set; }
        public virtual int PageNumber { get; set; }

        //[JsonIgnore]
        //public int PageIndex => PageNumber - 1;

        public virtual int Count { get; set; }
        public virtual IEnumerable<TModel> Data { get; set; }

    }
}

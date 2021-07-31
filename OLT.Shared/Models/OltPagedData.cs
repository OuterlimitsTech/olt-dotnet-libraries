using System.Collections.Generic;

namespace OLT.Core
{
    public class OltPagedData<TModel> : IOltPaged<TModel>
    {
        public virtual string SortBy { get; set; }
        public virtual bool Asc { get; set; }

        public virtual int PageSize => Size;
        public virtual int PageNumber => Page;

        public virtual int Size { get; set; }
        public virtual int Page { get; set; }

        //[JsonIgnore]
        //public int PageIndex => PageNumber - 1;

        public virtual int Count { get; set; }
        public virtual IEnumerable<TModel> Data { get; set; }

    }
}

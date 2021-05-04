using System;

namespace OLT.Core
{
    public class OltPagingParams : IOltPagingParams
    {

        private int? _page;
        public virtual int PageNumber
        {
            get => _page ?? 1;
            set => _page = Math.Max(value, 1);
        }

        private int? _pageSize;
        public virtual int PageSize
        {
            get => _pageSize ?? 10;
            set => _pageSize = Math.Max(value, 1);
        }
    }
}
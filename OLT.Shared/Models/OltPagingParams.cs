using System;

namespace OLT.Core
{
    public class OltPagingParams : IOltPagingParams
    {
        [Obsolete("Move to Page")]
        public virtual int PageNumber
        {
            get => Page;
            set => Page = value;
        }

        [Obsolete("Move to Size")]
        public virtual int PageSize
        {
            get => Size;
            set => Size = value;
        }

        private int? _page;
        public virtual int Page
        {
            get => _page ?? 1;
            set => _page = Math.Max(value, 1);
        }

        private int? _size;
        public virtual int Size
        {
            get => _size ?? 10;
            set => _size = Math.Max(value, 1);
        }
    }
}
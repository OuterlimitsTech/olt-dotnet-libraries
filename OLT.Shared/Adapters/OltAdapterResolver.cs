using System;
using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{
    public class OltAdapterResolver : OltAdapterCore, IOltAdapterResolver
    {
        private readonly IOltAdapter[] _adapters;

        public OltAdapterResolver(IEnumerable<IOltAdapter> adapters)
        {
            this._adapters = adapters.ToArray();
        }


        public virtual IOltDataAdapter<TSource, TModel> GetAdapter<TSource, TModel>()
            where TSource : class
            where TModel : class, new()
        {
            var adapterName = base.BuildName<TSource, TModel>();
            var adapter = this._adapters.FirstOrDefault(p => p.Name == adapterName);
            if (adapter == null)
            {
                throw new Exception($"Adapter Not Found {adapterName}");
            }

            return adapter as IOltDataAdapter<TSource, TModel>;
        }

        public virtual IOltAdapterQueryable<TSource, TModel> GetQueryableAdapter<TSource, TModel>()
            where TSource : class, IOltEntity
            where TModel : class, new()
        {
            var adapter = GetAdapter<TSource, TModel>();

            var queryableAdapter = adapter as IOltAdapterQueryable<TSource, TModel>;
            if (queryableAdapter == null)
            {
                return null;
            }

            return queryableAdapter;
        }

        public virtual IOltAdapterPaged<TSource, TModel> GetPagedAdapter<TSource, TModel>()
            where TSource : class, IOltEntity
            where TModel : class, new()
        {
            var adapter = GetAdapter<TSource, TModel>();

            var pagedAdapter = adapter as IOltAdapterPaged<TSource, TModel>;
            if (pagedAdapter == null)
            {
                throw new Exception("Paged Adapter Not Found");
            }

            return pagedAdapter;
        }
    }
}

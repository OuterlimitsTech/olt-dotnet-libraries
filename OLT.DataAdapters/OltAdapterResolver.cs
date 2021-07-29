using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltAdapterResolver : OltAdapterCore, IOltAdapterResolver
    {
        

        public OltAdapterResolver(IServiceProvider serviceProvider)
        {
            Adapters = serviceProvider.GetServices<IOltAdapter>().ToList();
        }

        protected virtual List<IOltAdapter> Adapters { get; }

        public virtual IOltDataAdapter<TSource, TModel> GetAdapter<TSource, TModel>()
        {
            var adapterName = base.BuildName<TSource, TModel>();
            var adapter = this.Adapters.FirstOrDefault(p => p.Name == adapterName);
            if (adapter == null)
            {
                throw new Exception($"Adapter Not Found {adapterName}");
            }

            return adapter as IOltDataAdapter<TSource, TModel>;
        }

        public virtual IOltAdapterQueryable<TSource, TModel> GetQueryableAdapter<TSource, TModel>()
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

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

        public virtual string GetAdapterName<TSource, TDestination>()
        {
            return base.BuildName<TSource, TDestination>();
        }

        #region [ Include ]

        public virtual IQueryable<TSource> Include<TSource, TDestination>(IQueryable<TSource> queryable)
            where TSource : class, IOltEntity
        {
            var adapter = this.GetAdapter<TSource, TDestination>(true);
            return this.Include(queryable, adapter);
        }

        protected virtual IQueryable<TEntity> Include<TEntity>(IQueryable<TEntity> queryable, IOltAdapter adapter)
        {
            if (adapter is IOltAdapterQueryableInclude<TEntity> includeAdapter)
            {
                return includeAdapter.Include(queryable);
            }

            return queryable;
        }

        #endregion

        #region [ Paged ]
        
        public virtual IOltPaged<TDestination> Paged<TSource, TDestination>(IQueryable<TSource> source, IOltPagingParams pagingParams)
            where TSource : class, IOltEntity
        {
            var adapter = GetPagedAdapter<TSource, TDestination>(true);
            if (pagingParams is IOltPagingWithSortParams pagingWithSortParams)
                return adapter.Map(source, pagingParams, pagingWithSortParams);
            return adapter.Map(source, pagingParams);
        }

        public virtual IOltPaged<TDestination> Paged<TSource, TDestination>(IQueryable<TSource> source, IOltPagingParams pagingParams, Func<IQueryable<TSource>, IQueryable<TSource>> orderBy) where TSource : class, IOltEntity
        {
            var adapter = GetPagedAdapter<TSource, TDestination>(true);
            return adapter.Map(source, pagingParams, orderBy);
        }

        #endregion

        #region [ ProjectTo Maps ]

        public virtual bool CanProjectTo<TEntity, TDestination>()
        {
            var name = GetAdapterName<TEntity, TDestination>();
            var adapter = GetAdapter(name, false);
            if (adapter is IOltAdapterQueryable<TEntity, TDestination> queryableAdapter)
            {
                return true;
            }

            return false;
        }

        public virtual IQueryable<TDestination> ProjectTo<TEntity, TDestination>(IQueryable<TEntity> source)
        {
            var name = GetAdapterName<TEntity, TDestination>();
            var adapter = GetAdapter(name, false);
            return ProjectTo<TEntity, TDestination>(source, adapter);
        }


        public virtual IQueryable<TDestination> ProjectTo<TEntity, TDestination>(IQueryable<TEntity> source, IOltAdapter adapter)
        {
            source = Include(source, adapter);
            if (adapter is IOltAdapterQueryable<TEntity, TDestination> queryableAdapter)
            {
                return queryableAdapter.Map(source);
            }

            throw new Exception($"{GetAdapterName<TEntity, TDestination>()} Adapter is not of type ${nameof(IOltAdapterQueryable<TEntity, TDestination>)}");
        }

        #endregion

        #region [ IEnumerable Maps ]

        public virtual IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source)
        {
            var name = GetAdapterName<TSource, TDestination>();
            var adapter = GetAdapter(name, true);
            return Map<TSource, TDestination>(source, adapter);
        }

        public virtual IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source, IOltAdapter adapter)
        {
            source = Include(source, adapter);

            switch (adapter)
            {
                case IOltAdapterQueryable<TSource, TDestination> queryableAdapter:
                    return ProjectTo<TSource, TDestination>(source);
                case IOltAdapter<TSource, TDestination> mapAdapter:
                    return mapAdapter.Map(source.ToList());
                default:
                    throw new Exception($"{GetAdapterName<TSource, TDestination>()} Adapter is not of type ${nameof(IOltAdapter<TSource, TDestination>)}");
            }
        }

        public virtual IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            var adapter = GetAdapter<TSource, TDestination>(false);
            return adapter == null ? GetAdapter<TDestination, TSource>(true).Map(source) : adapter.Map(source);
        }

        #endregion

        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var adapter = GetAdapter<TSource, TDestination>(false);
            if (adapter == null)
            {
                GetAdapter<TDestination, TSource>(true).Map(source, destination);
            }
            else
            {
                adapter.Map(source, destination);
            }
            return destination;
        }



        #region [ Get Adapater Methods ]

        protected virtual IOltAdapter<TSource, TDestination> GetAdapter<TSource, TDestination>(bool throwException)
        {
            return GetAdapter(this.GetAdapterName<TSource, TDestination>(), throwException) as IOltAdapter<TSource, TDestination>;
        }

        protected virtual IOltAdapter GetAdapter(string adapterName, bool throwException)
        {
            var adapter = Adapters.FirstOrDefault(p => p.Name == adapterName);
            if (adapter == null && throwException)
            {
                throw new Exception($"Adapter Not Found {adapterName}");
            }
            return adapter;
        }

        protected virtual IOltAdapterQueryable<TSource, TDestination> GetQueryableAdapter<TSource, TDestination>(bool throwException)
            where TSource : class, IOltEntity
            where TDestination : class
        {
            return GetAdapter<TSource, TDestination>(throwException) as IOltAdapterQueryable<TSource, TDestination>;
        }

        protected virtual IOltAdapterPaged<TSource, TDestination> GetPagedAdapter<TSource, TDestination>(bool throwException)
            where TSource : class, IOltEntity
        {
            var adapterName = GetAdapterName<TSource, TDestination>();
            var adapter = GetAdapter(adapterName, throwException);
            var pagedAdapter = adapter as IOltAdapterPaged<TSource, TDestination>;
            if (pagedAdapter == null && throwException)
            {
                throw new Exception($"{adapterName} Paged Adapter Not Found");
            }
            return pagedAdapter;
        }

        #endregion

        #region [ Obsolete Methods ]


        [Obsolete("Move to Map or ProjectTo")]
        public virtual IOltAdapter<TSource, TModel> GetAdapter<TSource, TModel>()
        {
            var adapterName = base.BuildName<TSource, TModel>();
            var adapter = this.Adapters.FirstOrDefault(p => p.Name == adapterName);
            if (adapter == null)
            {
                throw new Exception($"Adapter Not Found {adapterName}");
            }

            return adapter as IOltAdapter<TSource, TModel>;
        }

        [Obsolete("Move to Map or ProjectTo")]
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

        [Obsolete("Move to Map or ProjectTo")]
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

        #endregion
    }
}

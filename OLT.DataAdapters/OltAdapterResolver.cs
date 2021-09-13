using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var adapter = this.GetAdapter<TSource, TDestination>(false);
            if (adapter == null)
            {
                return queryable;
            }
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

        public virtual Func<IQueryable<TSource>, IQueryable<TSource>> DefaultOrderBy<TSource, TDestination>()
            where TSource : class, IOltEntity
        {
            return GetPagedAdapter<TSource, TDestination>(true).DefaultOrderBy;
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

        protected virtual IQueryable<TDestination> ProjectTo<TEntity, TDestination>(IQueryable<TEntity> source, IOltAdapter adapter)
        {
            source = Include(source, adapter);
            if (adapter is IOltAdapterQueryable<TEntity, TDestination> queryableAdapter)
            {
                return queryableAdapter.Map(source);
            }

            throw new OltAdapterNotFoundException(GetAdapterName<TEntity, TDestination>());
        }

        #endregion

        #region [ Maps ]

        public virtual IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source)
        {
            var name = GetAdapterName<TSource, TDestination>();
            var adapter = GetAdapter(name, true);
            return Map<TSource, TDestination>(source, adapter);
        }

        protected virtual IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source, IOltAdapter adapter)
        {
            source = Include(source, adapter);

            switch (adapter)
            {
                case IOltAdapterQueryable<TSource, TDestination> queryableAdapter:
                    return ProjectTo<TSource, TDestination>(source, queryableAdapter);
                case IOltAdapter<TSource, TDestination> mapAdapter:
                    return mapAdapter.Map(source.ToList());
                default:
                    throw new OltAdapterNotFoundException(GetAdapterName<TSource, TDestination>());
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
                throw new OltAdapterNotFoundException(adapterName);
            }
            return adapter;
        }

        protected virtual IOltAdapterPaged<TSource, TDestination> GetPagedAdapter<TSource, TDestination>(bool throwException)
            where TSource : class, IOltEntity
        {
            var adapterName = GetAdapterName<TSource, TDestination>();
            var adapter = GetAdapter(adapterName, throwException);
            var pagedAdapter = adapter as IOltAdapterPaged<TSource, TDestination>;
            if (pagedAdapter == null && throwException)
            {
                throw new OltAdapterNotFoundException($"{adapterName} Paged");
            }
            return pagedAdapter;
        }

        #endregion

    }
}

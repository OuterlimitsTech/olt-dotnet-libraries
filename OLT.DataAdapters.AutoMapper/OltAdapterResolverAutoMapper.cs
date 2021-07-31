using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltAdapterResolverAutoMapper : OltAdapterResolver
    {
        public OltAdapterResolverAutoMapper(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Mapper = serviceProvider.GetService<IMapper>();
        }

        protected virtual IMapper Mapper { get; }

        protected virtual bool HasAutoMap<TSource, TDestination>()
        {
            return Mapper.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() != null;
        }

        #region [ ProjectTo Maps ]

        public override bool CanProjectTo<TEntity, TDestination>()
        {
            return HasAutoMap<TEntity, TDestination>() || base.CanProjectTo<TEntity, TDestination>();
        }

        public override IQueryable<TDestination> ProjectTo<TEntity, TDestination>(IQueryable<TEntity> source, IOltAdapter adapter)
        {
            try
            {
                if (HasAutoMap<TEntity, TDestination>())
                {
                    return source.ProjectTo<TDestination>(Mapper.ConfigurationProvider);
                }
            }
            catch
            {

            }

            return base.ProjectTo<TEntity, TDestination>(source, adapter);
        }

        #endregion

        #region [ IEnumerable Maps ]

        public override IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source)
        {
            return HasAutoMap<TSource, TDestination>() ? source.ProjectTo<TDestination>(Mapper.ConfigurationProvider) : base.Map<TSource, TDestination>(source);
        }

        public override IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source, IOltAdapter adapter)
        {
            try
            {
                if (HasAutoMap<TSource, TDestination>())
                {
                    return source.ProjectTo<TDestination>(Mapper.ConfigurationProvider);
                }
            }
            catch
            {

            }

            return base.Map<TSource, TDestination>(source, adapter);
        }

        public override IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return HasAutoMap<TSource, TDestination>() ? Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source) : base.Map<TSource, TDestination>(source);
        }

        #endregion
        
        #region [ Paged ]

        public override IOltPaged<TDestination> Paged<TSource, TDestination>(IQueryable<TSource> source, IOltPagingParams pagingParams)
        {
            if (HasAutoMap<TSource, TDestination>())
            {
                var mapAdapter = GetPagedAdapterMap<TSource, TDestination>(true);
                Func<IQueryable<TSource>, IQueryable<TSource>> orderBy = orderByQueryable => orderByQueryable.OrderBy(null, mapAdapter.DefaultOrderBy);
                if (pagingParams is IOltPagingWithSortParams pagingWithSortParams)
                {
                    orderBy = orderByQueryable => orderByQueryable.OrderBy(pagingWithSortParams, mapAdapter.DefaultOrderBy);
                }
                return this.Paged<TSource, TDestination>(source, pagingParams, orderBy);
            }

            return base.Paged<TSource, TDestination>(source, pagingParams);
        }

        public override IOltPaged<TDestination> Paged<TSource, TDestination>(IQueryable<TSource> source, IOltPagingParams pagingParams, Func<IQueryable<TSource>, IQueryable<TSource>> orderBy)
        {
            if (HasAutoMap<TSource, TDestination>())
            {
                return source.OrderBy(null, orderBy).ProjectTo<TDestination>(Mapper.ConfigurationProvider).ToPaged(pagingParams);
            }
            return base.Paged<TSource, TDestination>(source, pagingParams, orderBy);
        }

        protected virtual IOltAdapterPagedMap<TSource, TDestination> GetPagedAdapterMap<TSource, TDestination>(bool throwException)
        {
            var adapterName = GetAdapterName<TSource, TDestination>();
            var adapter = GetAdapter(adapterName, throwException);
            var mapAdapter = adapter as IOltAdapterPagedMap<TSource, TDestination>;
            if (mapAdapter == null && throwException)
            {
                throw new Exception($"{adapterName} not of type {nameof(IOltAdapterPagedMap<TSource, TDestination>)}");
            }
            return mapAdapter;

        }

        #endregion

        public override TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            try
            {
                if (HasAutoMap<TSource, TDestination>())
                {
                    return Mapper.Map(source, destination);
                }
            }
            catch
            {

            }

            return base.Map(source, destination);
        }
    }
}
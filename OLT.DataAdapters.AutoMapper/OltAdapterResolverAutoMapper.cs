using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    public class OltAdapterResolverAutoMapper : OltAdapterResolver, IOltAdapterResolverAutoMapper
    {
        public OltAdapterResolverAutoMapper(
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Mapper = serviceProvider.GetService<IMapper>();
        }

        protected virtual IMapper Mapper { get; }

        protected virtual bool HasAutoMap<TSource, TDestination>()
        {
            return Mapper.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() != null;
        }

        protected virtual OltAutoMapperException<TSource, TResult> BuildException<TSource, TResult>(Exception exception)
        {
            if (exception is AutoMapperMappingException autoMapperException)
            {
                return new OltAutoMapperException<TSource, TResult>(autoMapperException);
            }
            return new OltAutoMapperException<TSource, TResult>(exception);
        }

        public override IQueryable<TSource> Include<TSource, TDestination>(IQueryable<TSource> queryable)
        {
            var adapter = GetAdapter(this.GetAdapterName<TSource, TDestination>(), false);
            return adapter == null ? queryable : this.Include(queryable, adapter);
        }

        #region [ ProjectTo Maps ]

        public override bool CanProjectTo<TEntity, TDestination>()
        {
            return HasAutoMap<TEntity, TDestination>() || base.CanProjectTo<TEntity, TDestination>();
        }

        protected virtual IQueryable<TDestination> ProjectFromQueryable<TEntity, TDestination>(IQueryable<TEntity> source)
        {
            try
            {
                return source.ProjectTo<TDestination>(Mapper.ConfigurationProvider);
            }
            catch (Exception exception)
            {
                throw BuildException<TEntity, TDestination>(exception);
            }
        }

        protected override IQueryable<TDestination> ProjectTo<TEntity, TDestination>(IQueryable<TEntity> source, IOltAdapter adapter)
        {
            if (HasAutoMap<TEntity, TDestination>())
            {
                return ProjectFromQueryable<TEntity, TDestination>(source);
            }

            return base.ProjectTo<TEntity, TDestination>(source, adapter);
        }


        #endregion

        #region [ Maps ]

        public override List<TDestination> Map<TSource, TDestination>(List<TSource> source)
        {
            if (HasAutoMap<TSource, TDestination>())
            {
                try
                {
                    return Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source.AsEnumerable()).ToList();
                }
                catch (Exception exception)
                {
                    throw BuildException<TSource, TDestination>(exception);
                }
            }

            return base.Map<TSource, TDestination>(source);
        }

        public override IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source)
        {
            return HasAutoMap<TSource, TDestination>() ? ProjectFromQueryable<TSource, TDestination>(source) : base.Map<TSource, TDestination>(source);
        }

        protected override IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source, IOltAdapter adapter)
        {
            return HasAutoMap<TSource, TDestination>() ? ProjectFromQueryable<TSource, TDestination>(source) : base.Map<TSource, TDestination>(source, adapter);
        }

        public override TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (HasAutoMap<TSource, TDestination>())
            {
                try
                {
                    return Mapper.Map(source, destination);
                }
                catch (Exception exception)
                {
                    throw BuildException<TSource, TDestination>(exception);
                }
            }

            return base.Map(source, destination);
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
                try
                {
                    return source.OrderBy(null, orderBy).ProjectTo<TDestination>(Mapper.ConfigurationProvider).ToPaged(pagingParams);
                }
                catch (Exception ex)
                {
                    throw BuildException<TSource, TDestination>(ex);
                }
            }
            return base.Paged<TSource, TDestination>(source, pagingParams, orderBy);
        }

        protected virtual IOltAdapterPagedMap<TSource, TDestination> GetPagedAdapterMap<TSource, TDestination>(bool throwException)
        {
            var adapterName = GetAdapterName<TSource, TDestination>();
            var adapter = GetAdapter(adapterName, false);
            var mapAdapter = adapter as IOltAdapterPagedMap<TSource, TDestination>;
            if (mapAdapter == null && throwException)
            {
                throw new OltAdapterNotFoundException($"{adapterName} Paged");
            }
            return mapAdapter;

        }

        public override Func<IQueryable<TSource>, IQueryable<TSource>> DefaultOrderBy<TSource, TDestination>()
        {
            return HasAutoMap<TSource, TDestination>() ? GetPagedAdapterMap<TSource, TDestination>(true).DefaultOrderBy : base.DefaultOrderBy<TSource, TDestination>();
        }

        #endregion
    }
}
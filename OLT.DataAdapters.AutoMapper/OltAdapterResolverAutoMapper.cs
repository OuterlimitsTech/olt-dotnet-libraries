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


        public override IQueryable<TDestination> ProjectTo<TEntity, TDestination>(IQueryable<TEntity> source, IOltAdapter adapter)
        {
            try
            {
                if (HasAutoMap<TEntity, TDestination>())
                {
                    var ad = GetAdapter<TEntity, TDestination>(false);

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
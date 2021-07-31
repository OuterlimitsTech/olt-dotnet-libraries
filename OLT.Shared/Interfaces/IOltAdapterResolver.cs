using System;
using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterResolver : IOltInjectableSingleton
    {
        bool CanProjectTo<TEntity, TDestination>();

        IQueryable<TSource> Include<TSource, TDestination>(IQueryable<TSource> queryable) where TSource : class, IOltEntity;

        IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> source, IOltAdapter adapter);
        IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> source);
        IOltPaged<TDestination> Paged<TSource, TDestination>(IQueryable<TSource> source, IOltPagingParams pagingParams) where TSource : class, IOltEntity;
        IOltPaged<TDestination> Paged<TSource, TDestination>(IQueryable<TSource> source, IOltPagingParams pagingParams, Func<IQueryable<TSource>, IQueryable<TSource>> orderBy) where TSource : class, IOltEntity;

        IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source, IOltAdapter adapter);
        IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        //[Obsolete("Move to Map or ProjectTo")]
        //IOltAdapter<TSource, TModel> GetAdapter<TSource, TModel>();
        ////IOltAdapterQueryable<TSource, TModel> GetQueryableAdapter<TSource, TModel>();
        //[Obsolete("Move to Map or ProjectTo")]
        //IOltAdapterPaged<TSource, TModel> GetPagedAdapter<TSource, TModel>();
    }
}
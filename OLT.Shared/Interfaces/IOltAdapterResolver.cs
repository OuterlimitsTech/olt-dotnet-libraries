using System;
using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterResolver : IOltInjectableSingleton
    {
        bool CanProjectTo<TEntity, TDestination>();

        IQueryable<TEntity> Include<TEntity, TDestination>(IQueryable<TEntity> queryable);
        IQueryable<TDestination> ProjectTo<TEntity, TDestination>(IQueryable<TEntity> source, IOltAdapter adapter);
        IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> source);
        IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source, IOltAdapter adapter);
        IEnumerable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        [Obsolete("Move to Map or ProjectTo")]
        IOltAdapter<TSource, TModel> GetAdapter<TSource, TModel>();
        //IOltAdapterQueryable<TSource, TModel> GetQueryableAdapter<TSource, TModel>();
        [Obsolete("Move to Map or ProjectTo")]
        IOltAdapterPaged<TSource, TModel> GetPagedAdapter<TSource, TModel>();
    }
}
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OLT.Core
{
    public static class OltAutomapperExtensions 
    {

        public static IMappingExpression<TSource, TDestination> AfterMap<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, IOltAdapterAfterMap<TSource, TDestination> afterMap)
            where TSource : class, IOltEntity
        {
            OltAfterMapConfig.Register(afterMap);
            return map;
        }

        public static IMappingExpression<TSource, TDestination> AfterMap<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map, Func<IQueryable<TDestination>, IQueryable<TDestination>> orderBy)
        {
            OltAfterMapConfig.Register<TSource, TDestination>(orderBy);
            return map;
        }
    }
}

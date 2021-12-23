using System;
using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{
    public static class OltAfterMapConfig 
    {
        private static readonly List<IOltAdapterAfterMap> _afterMapConfigs = new List<IOltAdapterAfterMap>();

        public static IQueryable<TDestination> ApplyAfterMaps<TSource, TDestination>(IQueryable<TDestination> queryable)
        {
            var name = $"{typeof(TSource).FullName}->{typeof(TDestination).FullName}";
            _afterMapConfigs.Where(p => p.Name == name)
                .ToList()
                .ForEach(item =>
                {
                    var afterMap = item as IOltAdapterAfterMap<TSource, TDestination>;
                    if (afterMap != null)
                    {
                        queryable = afterMap.AfterMap(queryable);
                    }
                });

            return queryable;
        }

        public static void Register<TSource, TDestination>(IOltAdapterAfterMap<TSource, TDestination> afterMap)
        {
            _afterMapConfigs.Add(afterMap);
        }

        public static void Register<TSource, TDestination>(Func<IQueryable<TDestination>, IQueryable<TDestination>> orderBy)
        {
            _afterMapConfigs.Add(new OltAfterMapOrderBy<TSource, TDestination>(orderBy));
        }        

    }
}
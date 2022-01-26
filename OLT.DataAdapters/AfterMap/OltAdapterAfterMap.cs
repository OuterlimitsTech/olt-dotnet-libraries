using System.Linq;

namespace OLT.Core
{
    public abstract class OltAdapterAfterMap<TSource, TDestination> : OltAdapterCore, IOltAdapterAfterMap<TSource, TDestination>
    {
        public string Name => base.BuildName<TSource, TDestination>();
        public abstract IQueryable<TDestination> AfterMap(IQueryable<TDestination> queryable);
    }
}
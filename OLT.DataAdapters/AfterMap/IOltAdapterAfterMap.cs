using System.Linq;

namespace OLT.Core
{
    public interface IOltAdapterAfterMap 
    {
        string Name { get; }
    }

    public interface IOltAdapterAfterMap<TSource, TDestination> : IOltAdapterAfterMap
    {
        IQueryable<TDestination> AfterMap(IQueryable<TDestination> queryable);
    }
}
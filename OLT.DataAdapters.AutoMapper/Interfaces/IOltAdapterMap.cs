using AutoMapper;

namespace OLT.Core
{
    public interface IOltAdapterMap : IProfileExpression
    {
        void CreateMaps();
    }


    public interface IOltAdapterMap<TSource, TDestination> : IOltAdapterMap
    {
        void BuildMap(IMappingExpression<TSource, TDestination> mappingExpression);
    }

}
using AutoMapper;

namespace OLT.Core
{
    public interface IOltAdapterMap2 : IProfileExpression
    {
        void CreateMaps();
    }


    public interface IOltAdapterMap2<TSource, TDestination> : IOltAdapterMap2
    {
        void BuildMap(IMappingExpression<TSource, TDestination> mappingExpression);
    }

    //public interface IOltBuildMap
    //{

    //}

    //public interface IOltBuildMap<TSource, TDestination> : IOltBuildMap
    //    where TSource: class
    //    where TDestination: class
    //{
    //    void BuildMap(IMappingExpression<TSource, TDestination> mappingExpression);
    //}
}
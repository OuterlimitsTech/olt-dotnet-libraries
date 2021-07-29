namespace OLT.Core
{
    //TODO: Move to DataAdapter package
    public interface IOltAdapterResolver : IOltInjectableSingleton
    {
        IOltDataAdapter<TSource, TModel> GetAdapter<TSource, TModel>();


        IOltAdapterQueryable<TSource, TModel> GetQueryableAdapter<TSource, TModel>();

        IOltAdapterPaged<TSource, TModel> GetPagedAdapter<TSource, TModel>();
    }
}
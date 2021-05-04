namespace OLT.Core
{
    public interface IOltAdapterResolver : IOltInjectableScoped
    {
        IOltDataAdapter<TSource, TModel> GetAdapter<TSource, TModel>()
            where TSource : class
            where TModel : class, new();


        IOltAdapterQueryable<TSource, TModel> GetQueryableAdapter<TSource, TModel>()
            where TSource : class, IOltEntity
            where TModel : class, new();

        IOltAdapterPaged<TSource, TModel> GetPagedAdapter<TSource, TModel>()
            where TSource : class, IOltEntity
            where TModel : class, new();
    }
}
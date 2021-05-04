namespace OLT.Core
{
    public interface IOltEntityIdService<TEntity> : IOltEntityService<TEntity>
        where TEntity : class, IOltEntityId, IOltEntity
    {
        TModel Get<TModel>(int id) where TModel : class, new();

        //IEnumerable<TResponseModel> Add<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> list)
        //    where TSaveModel : class, new()
        //    where TResponseModel : class, new();

        TModel Update<TModel>(int id, TModel model) where TModel : class, new();


        TResponseModel Update<TResponseModel, TSaveModel>(int id, TSaveModel model)
            where TResponseModel : class, new()
            where TSaveModel : class, new();

        bool SoftDelete(int id);

        int Count(IOltSearcher<TEntity> searcher);
    }
}
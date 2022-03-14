//using System.Threading.Tasks;

//namespace OLT.Core
//{
//    public interface IOltEntityIdService<TEntity> : IOltEntityService<TEntity>
//        where TEntity : class, IOltEntityId, IOltEntity
//    {
//        TModel Get<TModel>(int id) where TModel : class, new();

//        Task<TModel> GetAsync<TModel>(int id) where TModel : class, new();

//        TModel Update<TModel>(int id, TModel model) where TModel : class, new();

//        TResponseModel Update<TResponseModel, TSaveModel>(int id, TSaveModel model)
//            where TResponseModel : class, new()
//            where TSaveModel : class, new();

//        Task<TModel> UpdateAsync<TModel>(int id, TModel model) where TModel : class, new();

//        Task<TResponseModel> UpdateAsync<TResponseModel, TSaveModel>(int id, TSaveModel model)
//            where TResponseModel : class, new()
//            where TSaveModel : class, new();

//        bool SoftDelete(int id);
//        Task<bool> SoftDeleteAsync(int id);

//    }
//}
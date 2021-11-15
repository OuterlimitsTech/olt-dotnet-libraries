using System;
using System.Threading.Tasks;

namespace OLT.Core
{
    public interface IOltEntityUniqueIdService<TEntity> : IOltEntityService<TEntity>
        where TEntity : class, IOltEntityUniqueId, IOltEntity
    {
        TModel Get<TModel>(Guid uid) where TModel : class, new();
        Task<TModel> GetAsync<TModel>(Guid uid) where TModel : class, new();

        TModel Update<TModel>(Guid uid, TModel model) where TModel : class, new();

        TResponseModel Update<TResponseModel, TSaveModel>(Guid uid, TSaveModel model)
            where TResponseModel : class, new()
            where TSaveModel : class, new();

        Task<TModel> UpdateAsync<TModel>(Guid uid, TModel model) where TModel : class, new();

        Task<TResponseModel> UpdateAsync<TResponseModel, TSaveModel>(Guid uid, TSaveModel model)
            where TResponseModel : class, new()
            where TSaveModel : class, new();

        bool SoftDelete(Guid uid);
        Task<bool> SoftDeleteAsync(Guid uid);
    }
}
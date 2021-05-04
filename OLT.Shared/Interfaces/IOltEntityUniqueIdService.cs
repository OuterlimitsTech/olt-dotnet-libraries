using System;

namespace OLT.Core
{
    public interface IOltEntityUniqueIdService<TEntity> : IOltEntityService<TEntity>
        where TEntity : class, IOltEntityUniqueId, IOltEntity
    {
        TModel Get<TModel>(Guid uid) where TModel : class, new();

        //IEnumerable<TResponseModel> Add<TResponseModel, TSaveModel>(IEnumerable<TSaveModel> list)
        //    where TSaveModel : class, new()
        //    where TResponseModel : class, new();

        TModel Update<TModel>(Guid uid, TModel model) where TModel : class, new();


        TResponseModel Update<TResponseModel, TSaveModel>(Guid uid, TSaveModel model)
            where TResponseModel : class, new()
            where TSaveModel : class, new();
        
        bool SoftDelete(Guid uid);

    }
}
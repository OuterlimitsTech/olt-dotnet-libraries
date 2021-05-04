using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltCachedEntityService<TEntity> : IOltEntityService<TEntity>
        where TEntity : class, IOltEntity
    {
        IEnumerable<TEntity> GetAllFromCache(IOltSearcher<TEntity> searcher);
        IEnumerable<TModel> GetAllFromCache<TModel>(IOltSearcher<TEntity> searcher) where TModel : class, new();
    }
}
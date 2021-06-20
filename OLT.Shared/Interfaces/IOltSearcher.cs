namespace OLT.Core
{
    public interface IOltSearcher<TEntity> : IOltEntityQueryBuilder<TEntity>
        where TEntity : class, IOltEntity
    {
        bool IncludeDeleted { get; }
    }
}
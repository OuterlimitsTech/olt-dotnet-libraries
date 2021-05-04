// ReSharper disable once CheckNamespace
namespace OLT.Core
{

    public interface IOltCsvSeedModel<in TEntity>
        where TEntity: IOltEntity, IOltEntityId
    {
        int Id { get; }
        void Map(TEntity entity);
    }
}
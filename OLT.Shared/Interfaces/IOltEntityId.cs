namespace OLT.Core
{
    public interface IOltEntityId<T> : IOltEntity
    {
        T Id { get; set; }
    }

    public interface IOltEntityId : IOltEntityId<int>
    {
        
    }
}
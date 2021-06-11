namespace OLT.Core
{
    public interface IOltBuilder : IOltInjectableSingleton
    {
        string BuilderName { get; }
    }
}
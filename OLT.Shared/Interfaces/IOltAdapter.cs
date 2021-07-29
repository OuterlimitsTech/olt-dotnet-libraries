namespace OLT.Core
{

    //TODO: Move to DataAdapter package
    public interface IOltAdapter : IOltInjectableSingleton
    {
        string Name { get; }
    }
}
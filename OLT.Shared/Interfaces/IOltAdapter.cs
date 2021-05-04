namespace OLT.Core
{
    public interface IOltAdapter : IOltInjectableScoped
    {
        string Name { get; }
    }
}
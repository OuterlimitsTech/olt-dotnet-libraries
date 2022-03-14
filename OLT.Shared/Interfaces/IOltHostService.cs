namespace OLT.Core
{
    public interface IOltHostService : IOltInjectableSingleton
    {
        string ResolveRelativePath(string filePath);
        string EnvironmentName { get; }
        string ApplicationName { get; }
    }
}
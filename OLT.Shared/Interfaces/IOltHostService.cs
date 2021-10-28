namespace OLT.Core
{
    public interface IOltHostService
    {
        string ResolveRelativePath(string filePath);
        string EnvironmentName { get; }
        IOltEnvironment Environment { get; }
    }
}
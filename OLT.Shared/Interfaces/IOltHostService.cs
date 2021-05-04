namespace OLT.Core
{
    public interface IOltHostService
    {
        string ResolveRelativePath(string filePath);
        string EnvironmentName { get; }
        IOltConfigManager ConfigManager { get; }
        IOltEnvironment Environment { get; }
    }
}
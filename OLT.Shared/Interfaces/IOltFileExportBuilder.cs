namespace OLT.Core
{
    public interface IOltFileExportBuilder : IOltInjectableSingleton
    {
        string ExporterName { get; }
    }
}
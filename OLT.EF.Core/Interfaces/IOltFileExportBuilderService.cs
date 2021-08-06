namespace OLT.Core
{
    public interface IOltFileExportBuilderService : IOltFileExportBuilder, IOltCoreService
    {
        IOltFileBase64 Build<TParameterModel>(TParameterModel parameter) where TParameterModel : class, IOltGenericParameter;
    }
}
namespace OLT.Core
{
    public interface IOltFileBuilderService : IOltFileBuilder, IOltCoreService
    {
        IOltFileBase64 Build<TParameterModel>(TParameterModel parameter) where TParameterModel : class, IOltGenericParameter;
    }
}
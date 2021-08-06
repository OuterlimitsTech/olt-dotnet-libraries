namespace OLT.Core
{
    public abstract class OltFileExportBuilderService : OltDisposable, IOltFileExportBuilderService
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build<TParameterModel>(TParameterModel parameter) where TParameterModel : class, IOltGenericParameter;
    }
}
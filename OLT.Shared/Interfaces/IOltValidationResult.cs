namespace OLT.Core
{
    public interface IOltValidationResult
    {
        string ErrorMessage { get; set; }
        OltValidationSeverityTypes Severity { get; set; }
    }
}
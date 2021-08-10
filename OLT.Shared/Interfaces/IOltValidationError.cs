namespace OLT.Core
{
    public interface IOltValidationError
    {
        string ErrorMessage { get; set; }
        OltValidationSeverityTypes Severity { get; set; }
    }
}
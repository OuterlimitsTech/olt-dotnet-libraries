namespace OLT.Core
{
    public interface IOltValidationError
    {
        string Message { get; set; }
        OltValidationSeverityTypes Severity { get; set; }
    }
}
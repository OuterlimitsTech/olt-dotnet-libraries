namespace OLT.Core
{
    public interface IOltValidationError
    {
        string Message { get; set; }
        OltSeverityTypes Severity { get; set; }
    }
}
namespace OLT.Core
{
    public interface IOltValidationResult
    {
        string ErrorMessage { get; set; }
        //bool IsValid { get; }
        OltValidationSeverityTypes Severity { get; set; }
    }
}
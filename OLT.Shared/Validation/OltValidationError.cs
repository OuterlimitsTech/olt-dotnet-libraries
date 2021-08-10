namespace OLT.Core
{
    public class OltValidationError : IOltValidationError
    {
        public OltValidationError()
        {

        }

        public OltValidationError(string message)
        {
            this.ErrorMessage = message;
            this.Severity = string.IsNullOrEmpty(message) ? OltValidationSeverityTypes.Ok : OltValidationSeverityTypes.Error;
        }

        public string ErrorMessage { get; set; }
        public bool IsValid => string.IsNullOrEmpty(ErrorMessage);

        public OltValidationSeverityTypes Severity { get; set; }
    }
}

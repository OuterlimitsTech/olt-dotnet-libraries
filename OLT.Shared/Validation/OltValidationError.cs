namespace OLT.Core
{
    public class OltValidationError : IOltValidationError
    {
        public OltValidationError()
        {

        }

        public OltValidationError(string message)
        {
            this.Message = message;
            this.Severity = string.IsNullOrEmpty(message) ? OltValidationSeverityTypes.Ok : OltValidationSeverityTypes.Error;
        }

        public string Message { get; set; }
        public bool IsValid => string.IsNullOrEmpty(Message);

        public OltValidationSeverityTypes Severity { get; set; }
    }
}

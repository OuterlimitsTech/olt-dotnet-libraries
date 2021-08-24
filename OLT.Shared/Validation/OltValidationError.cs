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
            this.Severity = string.IsNullOrEmpty(message) ? OltSeverityTypes.Ok : OltSeverityTypes.Error;
        }

        public string Message { get; set; }
        public bool IsValid => string.IsNullOrEmpty(Message);

        public OltSeverityTypes Severity { get; set; }
    }
}

using System.Collections.Generic;

namespace OLT.Core
{
    public class OltValidationException : OltCustomException
    {
        public readonly IEnumerable<IOltValidationResult> Results;

        public OltValidationException(IEnumerable<IOltValidationResult> results, string errorMessage = "Please correct the validation errors")
            : base(OltExceptionTypes.ExpectationsFailed, errorMessage)
        {
            this.Results = results;
        }

        public OltValidationException(string errorMessage) : base(OltExceptionTypes.ExpectationsFailed, errorMessage)
        {
            //Results = new List<IOltValidationResult>
            //{
            //    new OltValidationResult(errorMessage)
            //};
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{
    public class OltResultValidation : IOltResultValidation
    {
        public OltResultValidation() { }

        public OltResultValidation(OltValidationSeverityTypes severity, string errorMessage)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Results.Add(new OltValidationResult
            {
                Severity = severity,
                ErrorMessage = errorMessage
            });
        }

        public virtual List<IOltValidationResult> Results { get; } = new List<IOltValidationResult>();
        public virtual bool Success => !Results.Any();
    }
}
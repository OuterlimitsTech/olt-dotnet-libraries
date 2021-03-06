using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{
    public class OltResultValidation : IOltResultValidation
    {
        public OltResultValidation() { }

        public OltResultValidation(OltSeverityTypes severity, string errorMessage)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Results.Add(new OltValidationError
            {
                Severity = severity,
                Message = errorMessage
            });
        }

        public bool Invalid => Results.Any();
        public virtual List<IOltValidationError> Results { get; } = new List<IOltValidationError>();
        public virtual bool Success => !Results.Any();
    }
}
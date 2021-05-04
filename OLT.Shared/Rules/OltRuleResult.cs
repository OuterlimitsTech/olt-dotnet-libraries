using System.Collections.Generic;
using System.Linq;

namespace OLT.Core
{

    public class OltRuleResult : IOltRuleResult
    {
        public OltRuleResult() { }

        public OltRuleResult(OltValidationSeverityTypes severity, string errorMessage)
        {
            Results.Add(new OltValidationResult
            {
                Severity = severity,
                ErrorMessage = errorMessage
            });
        }

        public List<IOltValidationResult> Results { get; } = new List<IOltValidationResult>();
        public bool Success => !Results.Any();
    }


}
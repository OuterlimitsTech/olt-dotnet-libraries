using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltResultValidation : IOltResult
    {
        List<IOltValidationError> Results { get; }
    }
}
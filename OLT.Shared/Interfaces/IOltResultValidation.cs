using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltResultValidation : IOltResult
    {
        bool Invalid { get; }
        List<IOltValidationError> Results { get; }
    }
}
using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltResult
    {
        bool Success { get; }
    }

    public interface IOltResultValidation : IOltResult
    {
        List<IOltValidationResult> Results { get; }
    }
}
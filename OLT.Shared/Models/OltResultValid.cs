using System.Collections.Generic;

namespace OLT.Core
{
    public class OltResultValid : IOltResultValidation
    {
        public virtual bool Success => true;
        public List<IOltValidationError> Results => new List<IOltValidationError>();
    }
}
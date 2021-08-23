using System.Collections.Generic;

namespace OLT.Core
{
    public class OltResultFailure : IOltResult
    {
        public OltResultFailure(string error)
        {
            Errors.Add(error);
        }

        public OltResultFailure(List<string> errors)
        {
            Errors = errors;
        }

        public virtual bool Success => false;

        public List<string> Errors { get; set; } = new List<string>();
    }
}
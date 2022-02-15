using System;

namespace OLT.Core
{
    public class OltValidationError : IOltValidationError
    {
   
        public OltValidationError(string message)
        {
            if (string.IsNullOrEmpty(message)) 
            {
                throw new ArgumentNullException(nameof(message));
            }
            this.Message = message;
        }

        public string Message { get; set; }
        public bool IsValid => string.IsNullOrEmpty(Message);

    }
}

using System;

namespace OLT.Core
{
    public class OltCustomException : Exception
    {
        private OltCustomException()
        {

        }

        public OltCustomException(OltExceptionTypes exceptionType, string message = null) : base(message)
        {
            ExceptionType = exceptionType;

        }
        public OltExceptionTypes ExceptionType { get; set; }


    }
}

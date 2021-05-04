namespace OLT.Core
{
    public class OltRecordNotFoundException : OltCustomException
    {

        public OltRecordNotFoundException(string message) : base(OltExceptionTypes.NotFound, message)
        {

        }

    
    }


    public class OltRecordNotFoundException<TServiceMessageEnum> : OltCustomException
        where TServiceMessageEnum : System.Enum
    {

        public OltRecordNotFoundException(string message) : base(OltExceptionTypes.NotFound, message)
        {
        }

        public OltRecordNotFoundException(TServiceMessageEnum messageType) : base(OltExceptionTypes.NotFound, $"{messageType.GetDescription()} Not Found")
        {

        }

    }
}
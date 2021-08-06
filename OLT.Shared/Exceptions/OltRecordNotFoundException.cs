using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace OLT.Core
{
    [Serializable]
    public class OltRecordNotFoundException : OltException
    {

        public OltRecordNotFoundException(string message) : base(message)
        {

        }

        #region [ Serializable Methods ]

        protected OltRecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        #endregion

    }


    [Serializable]
    public class OltRecordNotFoundException<TServiceMessageEnum> : OltRecordNotFoundException where TServiceMessageEnum : System.Enum
    {
        public OltRecordNotFoundException(TServiceMessageEnum messageType) : base($"{messageType.GetDescription()} Not Found")
        {

        }

        #region [ Serializable Methods ]

        protected OltRecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        #endregion
    }
}
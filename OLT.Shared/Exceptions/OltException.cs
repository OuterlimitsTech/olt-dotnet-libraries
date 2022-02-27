////using System;
////using System.Runtime.Serialization;
////using System.Security.Permissions;

////namespace OLT.Core
////{
////    [Serializable]
////    public class OltException : SystemException
////    {

////        public OltException(string message) : base(message)
////        {

////        }

////        public OltException(string message, Exception innerException) : base(message, innerException)
////        {

////        }

////        #region [ Serializable Methods ]

////        protected OltException(SerializationInfo info, StreamingContext context) : base(info, context)
////        {

////        }

////        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
////        public override void GetObjectData(SerializationInfo info, StreamingContext context)
////        {
////            base.GetObjectData(info, context);
////        }

////        #endregion
////    }
////}

////using System;
////using System.Collections.Generic;
////using System.Runtime.Serialization;
////using System.Security.Permissions;

////namespace OLT.Core
////{
////    [Serializable]
////    public class OltValidationException : OltException
////    {
////        public readonly IEnumerable<IOltValidationError> Results;

////        public OltValidationException(IEnumerable<IOltValidationError> results, string errorMessage = "Please correct the validation errors") : base(errorMessage)
////        {
////            this.Results = results;
////        }

////        #region [ Serializable Methods ]

////        protected OltValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
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
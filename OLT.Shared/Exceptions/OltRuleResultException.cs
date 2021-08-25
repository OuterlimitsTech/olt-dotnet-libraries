using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace OLT.Core
{
    [Serializable]
    public class OltRuleException : OltException
    {

        public OltRuleException(string errorMessage) : base(errorMessage)
        {
            
        }

        #region [ Serializable Methods ]

        protected OltRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
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
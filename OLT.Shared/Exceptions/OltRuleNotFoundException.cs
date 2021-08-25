using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace OLT.Core
{
    [Serializable]
    public class OltRuleNotFoundException : OltException
    {

        public OltRuleNotFoundException(string message) : base(message)
        {
            
        }

        public OltRuleNotFoundException(Type ruleType) : base($"{ruleType.FullName} rule not found")
        {

        }

        #region [ Serializable Methods ]

        protected OltRuleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
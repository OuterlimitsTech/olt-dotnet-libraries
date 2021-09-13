using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace OLT.Core
{
    [Serializable]
    public class OltAdapterNotFoundException : OltException
    {
        public OltAdapterNotFoundException(string adapterName) : base($"Adapter Not Found {adapterName}")
        {

        }

        
        #region [ Serializable Methods ]

        protected OltAdapterNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
    public class OltAdapterNotFoundException<TSource, TDestination> : OltAdapterNotFoundException
    {
        public OltAdapterNotFoundException() : base($"Adapter Not Found {typeof(TSource).FullName} -> {typeof(TDestination).FullName}")
        {

        }


        #region [ Serializable Methods ]

        protected OltAdapterNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
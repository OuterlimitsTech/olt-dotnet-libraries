using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using AutoMapper;

namespace OLT.Core
{
    [Serializable]
    public class OltAutoMapperException<TSource, TDestination> : OltException
    {
        public OltAutoMapperException(AutoMapperMappingException exception) :
            base($"AutoMapper Exception while using map {nameof(IOltAdapterMap<TSource, TDestination>)}: {typeof(TSource).FullName} -> {typeof(TDestination).FullName} {Environment.NewLine}{exception.Message}")
        {

        }

        public OltAutoMapperException(Exception exception) :
            base($"AutoMapper Exception while using map {nameof(IOltAdapterMap<TSource, TDestination>)}: {typeof(TSource).FullName} -> {typeof(TDestination).FullName} {Environment.NewLine}{exception}")
        {
            
        }

        #region [ Serializable Methods ]

        protected OltAutoMapperException(SerializationInfo info, StreamingContext context) : base(info, context)
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
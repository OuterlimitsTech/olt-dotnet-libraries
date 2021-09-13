using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using AutoMapper;

namespace OLT.Core
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class OltAutoMapperException<TSource, TDestination> : OltException
#pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        public OltAutoMapperException(AutoMapperMappingException exception) :
            base($"AutoMapper Exception while using map {nameof(IOltAdapterMap<TSource, TDestination>)}: {typeof(TSource).FullName} -> {typeof(TDestination).FullName} {Environment.NewLine}{exception.Message}", exception)
        {

        }

        public OltAutoMapperException(Exception exception) :
            base($"AutoMapper Exception while using map {nameof(IOltAdapterMap<TSource, TDestination>)}: {typeof(TSource).FullName} -> {typeof(TDestination).FullName}", exception)
        {
            
        }

    }
}
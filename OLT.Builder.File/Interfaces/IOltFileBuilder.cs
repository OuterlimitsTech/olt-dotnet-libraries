using System;

namespace OLT.Core
{
    public interface IOltFileBuilder : IOltBuilder
    {
        
    }

    public interface IOltFileBuilder<in TRequest, in TParameter> : IOltFileBuilder, IOltInjectableSingleton
       where TRequest : IOltRequest
       where TParameter : class
    {
        IOltFileBase64 Build(TRequest request, TParameter parameter);

    }

    public interface IOltFileBuilder<in TRequest> : IOltFileBuilder, IOltInjectableSingleton
        where TRequest : IOltRequest
    {
        IOltFileBase64 Build(TRequest request);
    }
}
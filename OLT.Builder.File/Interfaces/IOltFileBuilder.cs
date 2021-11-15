using System;

namespace OLT.Core
{
    public interface IOltFileBuilder : IOltBuilder
    {
        IOltFileBase64 Build<TRequest>(TRequest request) where TRequest : IOltRequest;
    }
}
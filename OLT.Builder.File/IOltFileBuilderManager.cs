using System;
using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltFileBuilderManager : IOltInjectableSingleton
    {
        List<IOltFileBuilder> Builders { get; }
        IOltFileBase64 Generate<TRequest>(TRequest request, string name) where TRequest : IOltRequest;
    }
}
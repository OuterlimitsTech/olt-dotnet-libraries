using System;
using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltFileBuilderManager : IOltInjectableSingleton
    {
        List<IOltFileBuilder> GetBuilders();
        IOltFileBase64 Generate<TRequest>(TRequest request, string name) where TRequest : IOltRequest;
    }
}
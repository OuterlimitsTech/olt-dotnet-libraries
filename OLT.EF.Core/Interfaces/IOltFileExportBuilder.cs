using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public interface IOltFileExportBuilder<in TRequest, in TParameter> : IOltFileExportBuilder, IOltInjectableSingleton
        where TRequest : IOltRequest
        where TParameter : class, IOltGenericParameter
    {
        IOltFileBase64 Build(TRequest request, TParameter parameter);

    }

    public interface IOltFileExportBuilder<in TRequest> : IOltFileExportBuilder, IOltInjectableSingleton
        where TRequest : IOltRequest
    {
        IOltFileBase64 Build(TRequest request);
    }
}
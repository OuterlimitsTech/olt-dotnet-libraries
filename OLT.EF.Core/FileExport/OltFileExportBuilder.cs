using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public abstract class OltFileExportBuilder<TRequest, TParameter> : OltDisposable, IOltFileExportBuilder<TRequest, TParameter>
        where TRequest : IOltRequest
        where TParameter : class, IOltGenericParameter
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TRequest request, TParameter parameter);
    }

    public abstract class OltFileExportBuilder<TRequest> : OltDisposable, IOltFileExportBuilder<TRequest>
        where TRequest : IOltRequest
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TRequest request);
    }
}
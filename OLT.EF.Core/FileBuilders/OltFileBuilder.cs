using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public abstract class OltFileBuilder<TRequest, TParameter> : OltDisposable, IOltFileBuilder<TRequest, TParameter>
        where TRequest : IOltRequest
        where TParameter : class, IOltGenericParameter
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TRequest request, TParameter parameter);
    }

    public abstract class OltFileBuilder<TRequest> : OltDisposable, IOltFileBuilder<TRequest>
        where TRequest : IOltRequest
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TRequest request);
    }
}
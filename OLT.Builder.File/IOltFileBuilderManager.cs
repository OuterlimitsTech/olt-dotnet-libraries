using System;

namespace OLT.Core
{
    public interface IOltFileBuilderManager : IOltInjectableSingleton
    {
        IOltFileBase64 Generate<TRequest>(TRequest request, string name) where TRequest : IOltRequest;

        IOltFileBase64 Generate<TRequest, TEnum>(TRequest request, Enum name)
            where TRequest : IOltRequest
            where TEnum : Enum;

        IOltFileBase64 Generate<TRequest, TParameterModel, TEnum>(TRequest request, TParameterModel parameters, TEnum name)
            where TRequest : IOltRequest
            where TParameterModel : class, IOltGenericParameter
            where TEnum : Enum;

        IOltFileBase64 Generate<TRequest, TParameterModel>(TRequest request, TParameterModel parameters, string name)
            where TRequest : IOltRequest
            where TParameterModel : class, IOltGenericParameter;

    }
}
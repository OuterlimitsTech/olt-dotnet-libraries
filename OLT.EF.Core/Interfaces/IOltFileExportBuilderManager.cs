using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public interface IOltFileExportBuilderManager : IOltInjectableSingleton
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


        ////IOltFileBase64 Generate<TRequest, TContext, TEntity>(TRequest request, IOltSearcher<TEntity> searcher, string name)
        ////    where TRequest : IOltRequest<TContext>
        ////    where TContext : class, IOltDbContext
        ////    where TEntity : class, IOltEntity;


        ////IOltFileBase64 Generate<TRequest, TContext, TEntity, TEnum>(TRequest request, IOltSearcher<TEntity> searcher, TEnum name)
        ////    where TRequest : IOltRequest<TContext>
        ////    where TContext : class, IOltDbContext
        ////    where TEntity : class, IOltEntity
        ////    where TEnum : Enum;
    }
}
using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public interface IOltFileExportBuilderManager : IOltInjectableSingleton
    {

        IOltFileBase64 Generate<TContext>(TContext context, string name) where TContext : DbContext, IOltDbContext;

        IOltFileBase64 Generate<TContext, TEnum>(TContext context, Enum name)
            where TContext : DbContext, IOltDbContext
            where TEnum : Enum;

        IOltFileBase64 Generate<TContext, TParameterModel, TEnum>(TContext context, TParameterModel parameters,
            TEnum name)
            where TContext : DbContext, IOltDbContext
            where TParameterModel : class, IOltGenericParameter
            where TEnum : Enum;

        IOltFileBase64 Generate<TContext, TParameterModel>(TContext context, TParameterModel parameters,
            string name)
            where TContext : DbContext, IOltDbContext
            where TParameterModel : class, IOltGenericParameter;


        IOltFileBase64 Generate<TContext, TEntity>(TContext context, IOltSearcher<TEntity> searcher, string name)
            where TContext : DbContext, IOltDbContext
            where TEntity : class, IOltEntity;


        IOltFileBase64 Generate<TContext, TEntity, TEnum>(TContext context, IOltSearcher<TEntity> searcher, TEnum name)
            where TContext : DbContext, IOltDbContext
            where TEntity : class, IOltEntity
            where TEnum : Enum;
    }
}
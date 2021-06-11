using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public interface IOltFileExportBuilder<in TContext, in TParameterModel> : IOltFileExportBuilder
        where TContext : DbContext, IOltDbContext
        where TParameterModel : class, IOltGenericParameter
    {
        IOltFileBase64 Build(TContext context, TParameterModel parameters);

    }

    public interface IOltFileExportBuilder<in TContext> : IOltFileExportBuilder
        where TContext : DbContext, IOltDbContext
    {
        IOltFileBase64 Build(TContext context);
    }


    public interface IOltFileExportBuilder<in TContext, in TParameterModel, in TServiceProvider> : IOltFileExportBuilder
        where TContext : DbContext, IOltDbContext
        where TParameterModel : class, IOltGenericParameter
        where TServiceProvider: IServiceProvider
    {
        IOltFileBase64 Build(TContext context, TParameterModel parameters, TServiceProvider serviceProvider);
    }
}
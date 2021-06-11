using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public abstract class OltFileExportBuilder<TContext, TParameterModel> : OltDisposable, IOltFileExportBuilder<TContext, TParameterModel>
        where TContext : DbContext, IOltDbContext
        where TParameterModel : class, IOltGenericParameter
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TContext context, TParameterModel parameter);
    }

    public abstract class OltFileExportBuilder<TContext> : OltDisposable, IOltFileExportBuilder<TContext>
        where TContext : DbContext, IOltDbContext
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TContext context);
    }


    public abstract class OltFileExportBuilder<TContext, TParameterModel, TServiceProvider> : OltDisposable, IOltFileExportBuilder<TContext, TParameterModel, TServiceProvider>
        where TContext : DbContext, IOltDbContext
        where TParameterModel : class, IOltGenericParameter
        where TServiceProvider : IServiceProvider
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TContext context, TParameterModel parameter, TServiceProvider serviceProvider);
    }
}
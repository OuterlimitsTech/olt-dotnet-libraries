using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public abstract class OltFileExportBuilder<TContext, TParameter> : OltDisposable, IOltFileExportBuilder<TContext, TParameter>
        where TContext : DbContext, IOltDbContext
        where TParameter : class, IOltGenericParameter
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TContext context, TParameter parameter);
    }

    public abstract class OltFileExportBuilder<TContext> : OltDisposable, IOltFileExportBuilder<TContext>
        where TContext : DbContext, IOltDbContext
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TContext context);
    }


    public abstract class OltFileExportBuilder<TContext, TParameter, TServiceProvider> : OltDisposable, IOltFileExportBuilder<TContext, TParameter, TServiceProvider>
        where TContext : DbContext, IOltDbContext
        where TParameter : class, IOltGenericParameter
        where TServiceProvider : IServiceProvider
    {
        public abstract string BuilderName { get; }
        public abstract IOltFileBase64 Build(TContext context, TParameter parameter, TServiceProvider serviceProvider);
    }
}
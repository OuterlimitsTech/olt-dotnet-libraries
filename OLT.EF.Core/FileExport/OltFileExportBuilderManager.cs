using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltFileExportBuilderManager : OltDisposable, IOltFileExportBuilderManager
    {
        private readonly List<IOltFileExportBuilder> _builders;

        public OltFileExportBuilderManager(IServiceProvider serviceProvider)
        {
            _builders = serviceProvider.GetServices<IOltFileExportBuilder>().ToList();
        }

        public IOltFileBase64 Generate<TContext>(TContext context, string name)
            where TContext : DbContext, IOltDbContext
        {
            var genericExporter = _builders.FirstOrDefault(p => p.BuilderName == name);

            if (genericExporter == null)
            {
                throw new Exception($"Exporter with name of {name} not found");
            }

            if (!(genericExporter is IOltFileExportBuilder<TContext> exporter))
            {
                throw new Exception($"Exporter typeof({nameof(IOltFileExportBuilder<TContext>)}) not found");
            }

            return exporter.Build(context);
        }

        public IOltFileBase64 Generate<TContext, TEnum>(TContext context, Enum name) 
            where TContext : DbContext, IOltDbContext 
            where TEnum : Enum
        {
            return this.Generate(context, name.GetCodeEnum());
        }

        public IOltFileBase64 Generate<TContext, TParameterModel, TEnum>(TContext context, TParameterModel parameters, TEnum name)
            where TContext : DbContext, IOltDbContext
            where TParameterModel : class, IOltGenericParameter
            where TEnum : Enum
        {
            return this.Generate(context, parameters, name.GetCodeEnum());
        }

        public IOltFileBase64 Generate<TContext, TParameterModel>(TContext context, TParameterModel parameters, string name)
            where TContext : DbContext, IOltDbContext
            where TParameterModel : class, IOltGenericParameter
        {
            var genericExporter = _builders.FirstOrDefault(p => p.BuilderName == name);

            if (genericExporter == null)
            {
                throw new Exception($"Exporter with name of {name} not found");
            }

            if (!(genericExporter is IOltFileExportBuilder<TContext, TParameterModel> exporter))
            {
                throw new Exception($"Exporter typeof({nameof(IOltFileExportBuilder<TContext, TParameterModel>)}) not found");
            }

            return exporter.Build(context, parameters);
        }

        public IOltFileBase64 Generate<TContext, TEntity>(TContext context, IOltSearcher<TEntity> searcher, string name)
            where TContext : DbContext, IOltDbContext
            where TEntity : class, IOltEntity
        {
            var genericExporter = _builders.FirstOrDefault(p => p.BuilderName == name);

            if (genericExporter == null)
            {
                throw new Exception($"Exporter with name of {name} not found");
            }

            if (!(genericExporter is IOltFileExportBuilderEntity<TContext, TEntity> exporter))
            {
                throw new Exception($"Exporter typeof({nameof(IOltFileExportBuilder<TContext>)}) not found");
            }

            return exporter.Build(context, context.GetQueryable(searcher));

        }

        public IOltFileBase64 Generate<TContext, TEntity, TEnum>(TContext context, IOltSearcher<TEntity> searcher, TEnum name) 
            where TContext : DbContext, IOltDbContext 
            where TEntity : class, IOltEntity where TEnum : Enum
        {
            return this.Generate(context, searcher, name.GetCodeEnum());
        }
    }
}
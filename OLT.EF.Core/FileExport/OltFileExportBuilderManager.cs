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

        public IOltFileBase64 Generate<TRequest>(TRequest request, string name)
            where TRequest : IOltRequest
        {
            var genericExporter = _builders.FirstOrDefault(p => p.BuilderName == name);

            if (genericExporter == null)
            {
                throw new OltException($"Exporter with name of {name} not found");
            }

            if (!(genericExporter is IOltFileExportBuilder<TRequest> exporter))
            {
                throw new OltException($"Exporter typeof({nameof(IOltFileExportBuilder<TRequest>)}) not found");
            }

            return exporter.Build(request);
        }

        public IOltFileBase64 Generate<TRequest, TEnum>(TRequest request, Enum name) 
            where TRequest : IOltRequest 
            where TEnum : Enum
        {
            return this.Generate(request, name.GetCodeEnum());
        }

        public IOltFileBase64 Generate<TRequest, TParameterModel, TEnum>(TRequest request, TParameterModel parameters, TEnum name)
            where TRequest : IOltRequest
            where TParameterModel : class, IOltGenericParameter
            where TEnum : Enum
        {
            return this.Generate(request, parameters, name.GetCodeEnum());
        }

        public IOltFileBase64 Generate<TRequest, TParameterModel>(TRequest request, TParameterModel parameters, string name)
            where TRequest : IOltRequest
            where TParameterModel : class, IOltGenericParameter
        {
            var genericExporter = _builders.FirstOrDefault(p => p.BuilderName == name);

            if (genericExporter == null)
            {
                throw new OltException($"Exporter with name of {name} not found");
            }

            if (!(genericExporter is IOltFileExportBuilder<TRequest, TParameterModel> exporter))
            {
                throw new OltException($"Exporter typeof({nameof(IOltFileExportBuilder<TRequest, TParameterModel>)}) not found");
            }

            return exporter.Build(request, parameters);
        }

        ////public IOltFileBase64 Generate<TRequest, TContext, TEntity>(TRequest request, IOltSearcher<TEntity> searcher, string name)
        ////    where TRequest : IOltRequest<TContext>
        ////    where TContext : class, IOltDbContext
        ////    where TEntity : class, IOltEntity
        ////{
        ////    var genericExporter = _builders.FirstOrDefault(p => p.BuilderName == name);

        ////    if (genericExporter == null)
        ////    {
        ////        throw new ArgumentNullException($"Exporter with name of {name} not found");
        ////    }

        ////    if (!(genericExporter is IOltFileExportBuilderEntity<TRequest, TContext, TEntity> exporter))
        ////    {
        ////        throw new InvalidCastException($"Exporter typeof({nameof(IOltFileExportBuilder)}) not found");
        ////    }

        ////    return exporter.Build(request, request.Context.GetQueryable(searcher));

        ////}

        ////public IOltFileBase64 Generate<TRequest, TContext, TEntity, TEnum>(TRequest request, IOltSearcher<TEntity> searcher, TEnum name)
        ////    where TRequest : IOltRequest<TContext>
        ////    where TContext : class, IOltDbContext
        ////    where TEntity : class, IOltEntity 
        ////    where TEnum : Enum
        ////{
        ////    return this.Generate<TRequest, TContext, TEntity>(request, searcher, name.GetCodeEnum());
        ////}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltFileBuilderManager : OltDisposable, IOltFileBuilderManager
    {
        private readonly List<IOltFileBuilder> _builders;

        public OltFileBuilderManager(IServiceProvider serviceProvider)
        {
            _builders = serviceProvider.GetServices<IOltFileBuilder>().ToList();
        }

        public IOltFileBase64 Generate<TRequest>(TRequest request, string name)
            where TRequest : IOltRequest
        {
            var genericExporter = _builders.FirstOrDefault(p => p.BuilderName == name);

            if (genericExporter == null)
            {
                throw new OltException($"FileBuilder with name of {name} not found");
            }

            if (!(genericExporter is IOltFileBuilder<TRequest> exporter))
            {
                throw new OltException($"FileBuilder typeof({nameof(IOltFileBuilder<TRequest>)}) not found");
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
                throw new OltException($"FileBuilder with name of {name} not found");
            }

            if (!(genericExporter is IOltFileBuilder<TRequest, TParameterModel> exporter))
            {
                throw new OltException($"FileBuilder typeof({nameof(IOltFileBuilder<TRequest, TParameterModel>)}) not found");
            }

            return exporter.Build(request, parameters);
        }

    }
}
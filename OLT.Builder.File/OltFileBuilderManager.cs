using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltFileBuilderManager : OltDisposable, IOltFileBuilderManager
    {
        private readonly Dictionary<string, IOltFileBuilder> _builders;
        public OltFileBuilderManager(IServiceProvider serviceProvider)
        {
            _builders = serviceProvider.GetServices<IOltFileBuilder>().ToList().ToDictionary(builder => builder.BuilderName, builder => builder);
        }

        public List<IOltFileBuilder> Builders => _builders.Values.ToList();

        /// <summary>
        /// Locates builder <paramref name="name"/> by using <see cref="IOltFileBuilder.BuilderName"/> and calls <see cref="IOltFileBuilder.Build{TRequest}(TRequest)"/>
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="OltFileBuilderNotFoundException"></exception>
        public virtual IOltFileBase64 Generate<TRequest>(TRequest request, string name)
            where TRequest : IOltRequest
        {
            if (_builders.ContainsKey(name))
            {
                return _builders[name].Build(request);
            }
            throw new OltFileBuilderNotFoundException(name);            
        }        

    }
}
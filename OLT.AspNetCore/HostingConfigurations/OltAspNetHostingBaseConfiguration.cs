using System;
using Microsoft.AspNetCore.Builder;

namespace OLT.Core
{

    public interface IOltAspNetHostingConfiguration
    {
        string Name { get; }

        IApplicationBuilder Configure<TSettings>(IApplicationBuilder app, TSettings settings,
            Action<IApplicationBuilder, TSettings> middlewareLogging)
            where TSettings : OltAspNetAppSettings;
    }

    public abstract class OltAspNetHostingBaseConfiguration : IOltAspNetHostingConfiguration
    {
        public abstract string Name { get; }

        public abstract IApplicationBuilder Configure<TSettings>(IApplicationBuilder app, TSettings settings,
            Action<IApplicationBuilder, TSettings> middlewareLogging)
            where TSettings : OltAspNetAppSettings;
    }
}
using System;
using Microsoft.AspNetCore.Builder;

namespace OLT.Core
{
    public class OltAspNetHostingConfiguration<TSettings>
        where TSettings : OltAspNetAppSettings
    {

        public OltAspNetHostingConfiguration(TSettings settings, Action<IApplicationBuilder, TSettings> middlewareLogging)
        {
            Settings = settings;
            MiddlewareLogging = middlewareLogging;
        }

        public TSettings Settings { get; }
        public Action<IApplicationBuilder, TSettings> MiddlewareLogging { get; }
    }
}
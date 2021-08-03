using System;
using Microsoft.AspNetCore.Builder;

namespace OLT.Core
{
    public abstract class OltAspNetHostingBaseConfiguration : IOltAspNetHostingConfiguration
    {
        public abstract string Name { get; }

        public abstract IApplicationBuilder Configure<TSettings>(IApplicationBuilder app, TSettings settings, Action action) where TSettings : OltAspNetAppSettings;
    }
}
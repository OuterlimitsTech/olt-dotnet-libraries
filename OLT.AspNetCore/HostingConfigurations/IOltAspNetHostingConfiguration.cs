﻿using System;
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
}
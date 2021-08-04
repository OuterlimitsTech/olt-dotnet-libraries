using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Core
{
    public class OltAspNetHostingNginxConfiguration : OltAspNetHostingDefaultConfiguration
    {
        public override string Name => OltAspNetDefaults.HostingConfigurations.Ngix;
    }
}
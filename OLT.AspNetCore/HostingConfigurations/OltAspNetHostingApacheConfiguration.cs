using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace OLT.Core
{
    public class OltAspNetHostingApacheConfiguration : OltAspNetHostingDefaultConfiguration
    {
        public override string Name => OltAspNetDefaults.HostingConfigurations.Apache;
    }
}
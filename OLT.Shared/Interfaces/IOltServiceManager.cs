using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    public interface IOltServiceManager : IOltInjectableScoped
    {
        IOltAdapterResolver AdapterResolver { get; }
        ILogger<OltServiceManager> LogService { get; }
        IOltMemoryCache MemoryCache { get; }
        IOltRuleManager RuleManager { get; }
    }
}
namespace OLT.Core
{
    public interface IOltServiceManager : IOltInjectableScoped
    {
        IOltAdapterResolver AdapterResolver { get; }
        IOltLogService LogService { get; }
        IOltMemoryCache MemoryCache { get; }
        IOltRuleManager RuleManager { get; }
    }
}
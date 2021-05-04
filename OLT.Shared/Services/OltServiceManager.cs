namespace OLT.Core
{
    public class OltServiceManager : OltDisposable, IOltServiceManager
    {

        public OltServiceManager(
            IOltMemoryCache memoryCache,
            IOltAdapterResolver adapterResolver,
            IOltRuleManager ruleManager,
            IOltLogService logService)
        {
            AdapterResolver = adapterResolver;
            LogService = logService;
            MemoryCache = memoryCache;
            RuleManager = ruleManager;
        }

        public IOltAdapterResolver AdapterResolver { get; }
        public IOltLogService LogService { get; }
        public IOltMemoryCache MemoryCache { get; }
        public IOltRuleManager RuleManager { get; }
    }
}
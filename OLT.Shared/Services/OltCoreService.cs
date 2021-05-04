namespace OLT.Core
{
    public abstract class OltCoreService : OltDisposable, IOltCoreService
    {
        protected OltCoreService(IOltServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
        }
        protected virtual IOltServiceManager ServiceManager { get; private set; }
    }
}
namespace OLT.Core
{
    public class OltEfCoreServiceManager : OltDisposable, IOltServiceManager
    {

        public OltEfCoreServiceManager(IOltAdapterResolver adapterResolver)
        {
            AdapterResolver = adapterResolver;
        }

        public virtual IOltAdapterResolver AdapterResolver { get; }

    }
}
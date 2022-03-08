using Microsoft.Extensions.Logging;
using System;

namespace OLT.Core
{
    public class OltServiceManager : OltDisposable, IOltServiceManager
    {

        public OltServiceManager(IOltAdapterResolver adapterResolver)
        {
            AdapterResolver = adapterResolver;
        }

        public IOltAdapterResolver AdapterResolver { get; }
        
    }
}
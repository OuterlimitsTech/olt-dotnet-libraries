using Microsoft.Extensions.Logging;
using System;

namespace OLT.Core
{
    public interface IOltServiceManager : IOltInjectableScoped
    {
        IOltAdapterResolver AdapterResolver { get; }
    }
}
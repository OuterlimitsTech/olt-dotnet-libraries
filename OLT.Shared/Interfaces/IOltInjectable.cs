using System;

namespace OLT.Core
{
    //public interface IOltInjectable : IDisposable
    //{
        
    //}

    public interface IOltInjectableScoped : IDisposable
    {

    }

    public interface IOltInjectableSingleton : IDisposable
    {

    }

    public interface IOltInjectableTransient : IDisposable
    {

    }
}
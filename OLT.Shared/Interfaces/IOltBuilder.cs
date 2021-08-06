using System;

namespace OLT.Core
{
    public interface IOltBuilder : IDisposable
    {
        string BuilderName { get; }
    }
}
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OLT.Email
{
    public interface IOltCarbonCopyEmailTemplate<out T>
        where T : class, IOltEmailAddress
    {
        IEnumerable<T> CarbonCopy { get; }
    }
}
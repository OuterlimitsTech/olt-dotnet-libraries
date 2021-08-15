using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace OLT.Email
{
    public interface IOltSingleEmailTemplate<out TEmailAddress> : IOltEmailTemplate
        where TEmailAddress : class, IOltEmailAddress
    {
        TEmailAddress To { get; }
    }

    public interface IOltSingleEmailTagTemplate<out TEmailAddress> : IOltEmailTagTemplate, IOltSingleEmailTemplate<TEmailAddress>
        where TEmailAddress : class, IOltEmailAddress
    {
        
    }
}
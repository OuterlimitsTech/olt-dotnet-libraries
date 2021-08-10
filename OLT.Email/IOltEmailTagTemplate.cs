using System.Collections.Generic;

namespace OLT.Email
{
    public interface IOltEmailTagTemplate : IOltEmailTemplate
    {
        IEnumerable<OltEmailTag> Tags { get; }
    }

    public interface IOltEmailTagTemplate<out TEmailAddress> : IOltEmailTagTemplate
        where TEmailAddress : class, IOltEmailAddress
    {
        IEnumerable<TEmailAddress> To { get; }
    }
}
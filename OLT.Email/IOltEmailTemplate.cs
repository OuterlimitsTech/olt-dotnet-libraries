using System.Collections.Generic;

namespace OLT.Email
{
    public interface IOltEmailTemplate
    {
        object GetTemplateData();
    }

    public interface IOltEmailTemplate<out TEmailAddress> : IOltEmailTemplate
        where TEmailAddress : class, IOltEmailAddress
    {
        IEnumerable<TEmailAddress> To { get; }
    }


}
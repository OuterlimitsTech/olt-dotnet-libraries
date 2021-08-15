using System.Collections.Generic;

namespace OLT.Email
{
    public interface IOltEmailTagTemplateRequest : IOltEmailTemplateRequest
    {
        IEnumerable<OltEmailTag> Tags { get; }
    }
}
using System.Collections.Generic;

namespace OLT.Email
{


    public interface IOltEmailJsonTemplate<out TEmailAddress, out TModel> : IOltEmailTemplate<TEmailAddress>
        where TEmailAddress : class, IOltEmailAddress
        where TModel : class
    {
        
        TModel TemplateData { get; }
    }
}
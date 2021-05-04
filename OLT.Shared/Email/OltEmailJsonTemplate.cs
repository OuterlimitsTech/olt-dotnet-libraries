using System.Collections.Generic;

namespace OLT.Email
{
    public abstract class OltEmailJsonTemplate<TEmailAddress, TModel> : IOltEmailJsonTemplate<TEmailAddress, TModel>
        where TEmailAddress : class, IOltEmailAddress
        where TModel : class

    {
        public abstract IEnumerable<TEmailAddress> To { get; set; }
        public abstract TModel TemplateData { get; set; }

        public object GetTemplateData()
        {
            return TemplateData;
        }
    }
}
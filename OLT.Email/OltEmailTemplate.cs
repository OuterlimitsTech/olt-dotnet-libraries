using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace OLT.Email
{
    public abstract class OltEmailTagTemplate<TEmailAddress> : IOltEmailTagTemplate<TEmailAddress>
        where TEmailAddress : class, IOltEmailAddress
    {
        public virtual IEnumerable<TEmailAddress> To { get; set; }
        public abstract IEnumerable<OltEmailTag> Tags { get; }

        public object GetTemplateData()
        {
            return OltEmailTag.ToDictionary(Tags.ToList());
        }
    }

}
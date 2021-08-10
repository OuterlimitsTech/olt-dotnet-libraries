using System.Collections.Generic;

namespace OLT.Email
{
    public class OltEmailRecipients
    {
        public virtual IEnumerable<IOltEmailAddress> To { get; set; } = new List<OltEmailAddress>();
        public virtual IEnumerable<IOltEmailAddress> CarbonCopy { get; set; } = new List<OltEmailAddress>();
    }
}
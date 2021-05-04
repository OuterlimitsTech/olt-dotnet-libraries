using System.Collections.Generic;

namespace OLT.Email
{
    public class OltEmailRecipients
    {
        public IEnumerable<IOltEmailAddress> To { get; set; } = new List<OltEmailAddress>();
        public IEnumerable<IOltEmailAddress> CarbonCopy { get; set; } = new List<OltEmailAddress>();
    }
}
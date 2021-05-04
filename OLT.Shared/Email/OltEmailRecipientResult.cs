using System.Collections.Generic;
using System.Linq;

namespace OLT.Email
{
    public class OltEmailRecipientResult
    {
        public OltEmailRecipientResult(OltEmailRecipients recipients, IOltEmailConfiguration configuration)
        {
            recipients.To?.ToList().ForEach(rec => To.Add(new OltEmailAddressResult(rec, configuration)));
            recipients.CarbonCopy?.ToList().ForEach(rec => CarbonCopy.Add(new OltEmailAddressResult(rec, configuration)));
        }

        public List<OltEmailAddressResult> To { get; set; } = new List<OltEmailAddressResult>();
        public List<OltEmailAddressResult> CarbonCopy { get; set; } = new List<OltEmailAddressResult>();
    }
}
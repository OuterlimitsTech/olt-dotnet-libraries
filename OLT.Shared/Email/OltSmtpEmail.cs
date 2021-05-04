using System.Collections.Generic;

namespace OLT.Email
{
    public class OltSmtpEmail : IOltSmtpEmail
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public IOltEmailAddress From { get; set; } = new OltEmailAddress();
        public List<IOltEmailAddress> To { get; set; } = new List<IOltEmailAddress>();
        public IOltSmtpConfiguration SmtpConfiguration { get; set; } = new OltSmtpConfiguration();
    }
}
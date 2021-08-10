using System.Collections.Generic;

namespace OLT.Email
{
    public class OltSmtpEmail : IOltSmtpEmail
    {
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual IOltEmailAddress From { get; set; } = new OltEmailAddress();
        public virtual List<IOltEmailAddress> To { get; set; } = new List<IOltEmailAddress>();
        public virtual IOltSmtpConfiguration SmtpConfiguration { get; set; } = new OltSmtpConfiguration();
    }
}
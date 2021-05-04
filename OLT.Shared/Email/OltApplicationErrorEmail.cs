using System.Collections.Generic;

namespace OLT.Email
{
    public class OltApplicationErrorEmail : IOltApplicationErrorEmail
    {
        public string AppName { get; set; }
        public string Environment { get; set; }
        public IOltEmailAddress From { get; set; } = new OltEmailAddress();
        public List<IOltEmailAddress> To { get; set; } = new List<IOltEmailAddress>();
        public IOltSmtpConfiguration SmtpConfiguration { get; set; } = new OltSmtpConfiguration();
    }
}
using OLT.Core;

namespace OLT.Email
{
    public class OltSmtpConfiguration : OltDisposable, IOltSmtpConfiguration
    {
        public OltSmtpConfiguration()
        {

        }

        public OltSmtpConfiguration(IOltSmtpConfiguration configuration)
        {
            SmtpServer = configuration.SmtpServer;
            SmtpPort = configuration.SmtpPort;
            SmtpUsername = configuration.SmtpUsername;
            SmtpPassword = configuration.SmtpPassword;
        }

        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
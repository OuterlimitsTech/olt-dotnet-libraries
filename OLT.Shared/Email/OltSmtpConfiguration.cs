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

        public virtual string SmtpServer { get; set; }
        public virtual int SmtpPort { get; set; }
        public virtual string SmtpUsername { get; set; }
        public virtual string SmtpPassword { get; set; }
    }
}
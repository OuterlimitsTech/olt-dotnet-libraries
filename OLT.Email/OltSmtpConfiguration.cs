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
            Server = configuration.Server;
            Port = configuration.Port;
            Username = configuration.Username;
            Password = configuration.Password;
            EnableSsl = configuration.EnableSsl;
        }

        public virtual string Server { get; set; }
        public virtual int Port { get; set; }
        public bool EnableSsl { get; set; } = true;
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
    }
}
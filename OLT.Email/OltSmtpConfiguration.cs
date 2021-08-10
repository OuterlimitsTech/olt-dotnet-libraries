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
            DisableSsl = configuration.DisableSsl;
        }

        public virtual string Server { get; set; }
        public virtual int Port { get; set; }
        public bool DisableSsl { get; set; } 
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
    }
}
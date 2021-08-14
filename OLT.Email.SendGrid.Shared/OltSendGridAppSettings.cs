using System;

namespace OLT.Email
{
    [Obsolete("Move to OltAppSettingsSendGrid")]
    public class OltSendGridAppSettings 
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ApiKey { get; set; }
        public bool Production { get; set; }
        public string DomainWhitelist { get; set; }
        public string EmailWhitelist { get; set; }
    }
}
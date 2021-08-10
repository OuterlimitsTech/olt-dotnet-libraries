using OLT.Core;

namespace OLT.Email
{
    
    public class OltSendGridAppSettings : IOltAppSettings
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ApiKey { get; set; }
        public bool Production { get; set; }
        public string DomainWhitelist { get; set; }
        public string EmailWhitelist { get; set; }
    }

    public class OltEmailTemplateRequestSendGrid : OltEmailTemplateRequest, IOltEmailTemplateRequestSendGrid
    {
        public virtual int? UnsubscribeGroupId { get; set; }
        public virtual object TemplateData { get; set; }
    }
}
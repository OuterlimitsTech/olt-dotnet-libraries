using OLT.Core;

namespace OLT.Email
{
    public class OltEmailTemplateRequestSendGrid : OltEmailTemplateRequest, IOltEmailTemplateRequestSendGrid
    {
        public virtual int? UnsubscribeGroupId { get; set; }
        public virtual object TemplateData { get; set; }
    }
}
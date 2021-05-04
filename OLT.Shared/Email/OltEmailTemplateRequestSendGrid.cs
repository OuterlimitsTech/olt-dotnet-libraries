namespace OLT.Email
{
    public class OltEmailTemplateRequestSendGrid : OltEmailTemplateRequest, IOltEmailTemplateRequestSendGrid
    {
        public int? UnsubscribeGroupId { get; set; }
        public object TemplateData { get; set; }
    }
}
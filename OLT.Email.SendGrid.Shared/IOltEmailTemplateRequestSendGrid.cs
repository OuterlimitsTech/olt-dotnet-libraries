namespace OLT.Email
{
    public interface IOltEmailTemplateRequestSendGrid : IOltEmailTemplateRequest
    {
        int? UnsubscribeGroupId { get; }
        object TemplateData { get; }
    }
}
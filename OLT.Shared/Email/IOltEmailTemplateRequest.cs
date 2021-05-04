

// ReSharper disable once CheckNamespace
namespace OLT.Email
{
    public interface IOltEmailTemplateRequest : IOltEmailRequest
    {
        string TemplateName { get; }
    }
}
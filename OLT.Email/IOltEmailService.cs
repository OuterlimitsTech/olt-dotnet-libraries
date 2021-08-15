using OLT.Core;

namespace OLT.Email
{
    public interface IOltEmailService : IOltInjectableScoped
    {
        OltEmailResult SendEmail<T>(T request) where T : IOltEmailTemplateRequest;
        OltEmailResult SendEmail<T>(T request, OltEmailAddress @from) where T : IOltEmailTemplateRequest;
        OltEmailResult SendEmail(IOltEmailCalendarRequest request, IOltSmtpConfiguration smtpConfiguration);
        OltEmailResult SendEmail(IOltEmailCalendarRequest request, IOltSmtpConfiguration smtpConfiguration, OltEmailAddress from);
    }
}
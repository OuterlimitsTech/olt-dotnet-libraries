using OLT.Core;

namespace OLT.Email
{
    public interface IOltEmailService : IOltInjectableScoped
    {
        //OltEmailResult SendEmail(IOltEmailTemplateRequest request);
        OltEmailResult SendEmail<T>(T request) where T : IOltEmailTemplateRequest;
        OltEmailResult SendEmail<T>(T request, OltEmailAddress @from) where T : IOltEmailTemplateRequest;
        //OltEmailResult SendEmail(IOltEmailTemplateRequest request, OltEmailAddress from);

        OltEmailResult SendEmail(IOltEmailCalendarRequest request, IOltSmtpConfiguration smtp);
        OltEmailResult SendEmail(IOltEmailCalendarRequest request, IOltSmtpConfiguration smtp, OltEmailAddress from);
    }
}
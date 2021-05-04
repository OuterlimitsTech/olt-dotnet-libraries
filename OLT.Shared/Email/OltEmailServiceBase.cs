using System;
using System.Collections.Generic;
using System.Linq;
using OLT.Core;

// ReSharper disable once CheckNamespace
namespace OLT.Email
{
    public abstract class OltEmailServiceBase<TConfig> : OltDisposable, IOltEmailService
        where TConfig : IOltEmailConfiguration
    {

        protected OltEmailServiceBase(TConfig configuration)
        {
            Configuration = configuration;
        }

        protected virtual TConfig Configuration { get; }

        protected virtual bool SendEmail(OltEmailResult result)
        {
            if (result.Errors.Any())
            {
                return false;
            }
            return result.RecipientResults.To?.Any(p => !p.Skipped) == true; 
        }

        public virtual OltEmailResult SendEmail<T>(T request) where T : IOltEmailTemplateRequest
        {
            return SendEmail(request, Configuration.From);
        }

        public virtual OltEmailResult SendEmail(IOltEmailCalendarRequest request, IOltSmtpConfiguration smtpConfiguration)
        {
            return SendEmail(request, smtpConfiguration, Configuration.From);
        }

        protected virtual List<string> Validate(IOltEmailRequest request, OltEmailAddress from)
        {
            var errors = new List<string>();
     

            var fromEmail = (from?.Email ?? Configuration.From.Email)?.Trim();
            var fromName = (from?.Name ?? Configuration.From.Name)?.Trim();

            if (string.IsNullOrWhiteSpace(fromEmail))
            {
                errors.Add("From Email Missing");
            }

            if (string.IsNullOrWhiteSpace(fromName))
            {
                errors.Add("From Name Missing");
            }

            if (!request.Recipients.To.Any())
            {
                errors.Add("Requires To Recipient");
            }

            if (request is IOltEmailTemplateRequest templateRequest)
            {
                if (string.IsNullOrWhiteSpace(templateRequest.TemplateName))
                {
                    errors.Add("Template Name Missing");
                }

            }

            if (request is IOltEmailCalendarRequest calendarRequest)
            {
                if (string.IsNullOrWhiteSpace(calendarRequest.Subject))
                {
                    errors.Add("Subject Missing");
                }

                if (!(calendarRequest.CalendarInvite?.Bytes.Length > 0))
                {
                    errors.Add("Calendar Invite Missing");
                }

            }

            return errors;
        }

        protected virtual bool HasRecipients(OltEmailRecipients recipients)
        {
            return recipients?.To?.Any() == true || recipients?.CarbonCopy?.Any() == true;
        }

   

        public abstract OltEmailResult SendEmail<T>(T request, OltEmailAddress @from) where T: IOltEmailTemplateRequest;
        public abstract OltEmailResult SendEmail(IOltEmailCalendarRequest request, IOltSmtpConfiguration smtpConfiguration, OltEmailAddress @from);
    }
}
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OLT.Email.SendGrid
{
    public class OltSendGridEmailService : OltEmailServiceBase<IOltEmailConfigurationSendGrid>
    {
        public OltSendGridEmailService(IOltEmailConfigurationSendGrid configuration) : base(configuration)
        {
        }


        protected virtual SendGridMessage CreateMessage(OltEmailResult result, OltEmailAddress from)
        {

            var fromEmailAddress = new EmailAddress(Configuration.From?.Email, Configuration.From?.Name);
            if (!string.IsNullOrWhiteSpace(from.Email))
            {
                fromEmailAddress.Email = from.Email;
            }
            if (!string.IsNullOrWhiteSpace(from.Name))
            {
                fromEmailAddress.Name = from.Name;
            }

            var msg = new SendGridMessage();
            msg.SetFrom(fromEmailAddress);


            ConfigureRecipients(msg, result.RecipientResults);

            return msg;
        }

        protected virtual OltEmailResult CreateResult(IOltEmailRequest request, OltEmailAddress from)
        {
            var result = new OltEmailResult
            {
                Errors = Validate(request, from),
                RecipientResults = new OltEmailRecipientResult(request.Recipients, Configuration)
            };

            if (string.IsNullOrWhiteSpace(Configuration.ApiKey))
            {
                result.Errors.Add("SendGrid API Key Missing");
            }

            return result;
        }

        protected virtual OltEmailResult CreateResult(IOltEmailTemplateRequest request, OltEmailAddress from)
        {
            var result = new OltEmailResult
            {
                Errors = Validate(request, from),
                RecipientResults = new OltEmailRecipientResult(request.Recipients, Configuration)
            };

            if (string.IsNullOrWhiteSpace(Configuration.ApiKey))
            {
                result.Errors.Add("SendGrid API Key Missing");
            }

            return result;
        }

        public override OltEmailResult SendEmail<T>(T request, OltEmailAddress from)
        {
            var result = CreateResult(request, from);

            if (!SendEmail(result))
            {
                return result;
            }

            var msg = CreateMessage(result, from);

            msg.SetTemplateId(request.TemplateName);

            if (request is IOltEmailTemplateRequestSendGrid sendGridRequest)
            {
                if (sendGridRequest.UnsubscribeGroupId.HasValue)
                {
                    msg.SetAsm(sendGridRequest.UnsubscribeGroupId.Value);
                }

                if (sendGridRequest.TemplateData != null)
                {
                    msg.SetTemplateData(sendGridRequest.TemplateData);
                }
            }

            if (Configuration.ClickTracking)
            {
                msg.SetClickTracking(true, true);
            }


            // ReSharper disable once SuspiciousTypeConversion.Global
            if (request is IOltEmailAttachmentRequest attachmentRequest)
            {
                attachmentRequest.Attachments?.ToList()?.ForEach(attachment =>
                {
                    msg.AddAttachment(attachment.FileName, Convert.ToBase64String(attachment.Bytes), attachment.ContentType);
                });
            }

            return Send(msg, result);
        }




        public override OltEmailResult SendEmail(IOltEmailCalendarRequest request, IOltSmtpConfiguration smtpConfiguration, OltEmailAddress @from)
        {
            var result = CreateResult(request, from);

            if (!SendEmail(result))
            {
                return result;
            }


            using (var smtp = new System.Net.Mail.SmtpClient(smtpConfiguration.Server, smtpConfiguration.Port))
            {
                smtp.EnableSsl = smtpConfiguration.EnableSsl;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password);

                var msg = new System.Net.Mail.MailMessage
                {
                    From = new MailAddress(from.Email, from.Name),
                    Subject = request.Subject,
                    Body = request.Body
                };

                request.Recipients.To.ToList().ForEach(rec =>
                {
                    msg.To.Add(new MailAddress(rec.Email, rec.Name));
                });

                request.Recipients.CarbonCopy.ToList().ForEach(rec =>
                {
                    msg.To.Add(new MailAddress(rec.Email, rec.Name));
                });

                System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType("text/calendar");
                contentType.Parameters.Add("method", "REQUEST");
                msg.Headers.Add("Content-class", "urn:content-classes:calendarmessage");
                contentType.Parameters.Add("name", "invite.ics");
                AlternateView avCal = AlternateView.CreateAlternateViewFromString(Encoding.UTF8.GetString(request.CalendarInvite.Bytes, 0, request.CalendarInvite.Bytes.Length), contentType);
                msg.AlternateViews.Add(avCal);

                try
                {
                    smtp.Send(msg);
                }
                catch (Exception exception)
                {
                    result.Errors.Add($"Error - {exception}");
                }
                finally
                {
                    msg.Dispose();
                }
            }

            return result;
        }

        protected virtual OltEmailResult Send(SendGridMessage msg, OltEmailResult result)
        {
            var client = new SendGridClient(Configuration.ApiKey);
            var sendResponse = client.SendEmailAsync(msg).Result;
            if (sendResponse.StatusCode != HttpStatusCode.Accepted)
            {
                result.Errors.Add($"{sendResponse.StatusCode.ToString()} - {sendResponse.Body.ReadAsStringAsync().Result}");
            }

            return result;
        }





        protected virtual void ConfigureRecipients(SendGridMessage msg, OltEmailRecipientResult recipients)
        {
            recipients.To?.Where(p => !p.Skipped && string.IsNullOrWhiteSpace(p.Error)).ToList().ForEach(rec =>
            {
                msg.AddTo(new EmailAddress(rec.Email, rec.Name));
            });

            recipients.CarbonCopy?.Where(p => !p.Skipped && string.IsNullOrWhiteSpace(p.Error)).ToList().ForEach(rec =>
            {
                msg.AddCc(new EmailAddress(rec.Email, rec.Name));
            });

        }

    }
}

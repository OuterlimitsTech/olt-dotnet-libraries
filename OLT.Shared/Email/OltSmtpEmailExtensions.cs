using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using OLT.Email;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{

    public static class OltSmtpEmailExtensions
    {
        /// <summary>
        /// Sends email using SendGrid SMTP server using api key
        /// </summary>
        /// <param name="email"></param>
        /// <param name="message"></param>
        public static void OltEmail(IOltSmtpEmail email, string message)
        {

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(email.From.Email, email.From.Name)
            };


            email.To.ToList().ForEach(rec => mailMessage.To.Add(new MailAddress(rec.Email, rec.Name)));
            mailMessage.Body = message;
            mailMessage.Subject = email.Subject;





            try
            {
                using (var client = new SmtpClient(email.SmtpConfiguration.SmtpServer))
                {
                    client.UseDefaultCredentials = false;
                    client.Port = email.SmtpConfiguration.SmtpPort;
                    client.Credentials = new NetworkCredential(email.SmtpConfiguration.SmtpUsername, email.SmtpConfiguration.SmtpPassword);
                    client.Send(mailMessage);
                }
            }
            catch (Exception mailException)
            {
                Console.Write(mailException);
            }


        }

        /// <summary>
        /// Sends email with exception using SendGrid SMTP server using api key
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="email"></param>
        public static void OltEmailError(this Exception ex, IOltApplicationErrorEmail email)
        {

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@outerlimitstech.com")
            };

            email.To.ToList().ForEach(rec => mailMessage.To.Add(new MailAddress(rec.Email, rec.Name)));
            mailMessage.Body = $"The following error occurred:{Environment.NewLine}{ex}";
            mailMessage.Subject = $"APPLICATION ERROR on {email.AppName} {email.Environment} Environment occurred at {DateTimeOffset.Now:f}";

            try
            {

                using (var client = new SmtpClient(email.SmtpConfiguration.SmtpServer))
                {
                    client.UseDefaultCredentials = false;
                    client.Port = email.SmtpConfiguration.SmtpPort;
                    client.Credentials = new NetworkCredential(email.SmtpConfiguration.SmtpUsername, email.SmtpConfiguration.SmtpPassword);
                    client.Send(mailMessage);
                }

            }
            catch (Exception mailException)
            {
                Console.Write(mailException);
            }


        }
    }
}
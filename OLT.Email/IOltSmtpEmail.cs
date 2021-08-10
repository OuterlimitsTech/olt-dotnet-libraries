using System.Collections.Generic;

namespace OLT.Email
{
    public interface IOltSmtpEmail 
    {
        string Subject { get; }
        string Body { get; }
        IOltEmailAddress From { get; }
        List<IOltEmailAddress> To { get; }
        IOltSmtpConfiguration SmtpConfiguration { get; }
    }
}
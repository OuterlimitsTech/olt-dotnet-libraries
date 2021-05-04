using System.Collections.Generic;

namespace OLT.Email
{
    public interface IOltApplicationErrorEmail 
    {
        string AppName { get; } 
        string Environment { get; }
        IOltEmailAddress From { get; }
        List<IOltEmailAddress> To { get; }
        IOltSmtpConfiguration SmtpConfiguration { get; }
    }
}
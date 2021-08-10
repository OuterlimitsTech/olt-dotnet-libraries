using System;
using OLT.Core;

namespace OLT.Email
{
    public interface IOltSmtpConfiguration : IDisposable
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; }
        string SmtpPassword { get; }
    }
}
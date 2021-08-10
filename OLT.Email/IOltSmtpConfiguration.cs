using System;
using OLT.Core;

namespace OLT.Email
{
    public interface IOltSmtpConfiguration : IDisposable
    {
        string Server { get; }
        bool EnableSsl { get; }
        int Port { get; }
        string Username { get; }
        string Password { get; }
    }
}
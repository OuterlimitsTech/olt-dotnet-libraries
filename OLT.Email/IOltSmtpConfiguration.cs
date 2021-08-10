using System;
using OLT.Core;

namespace OLT.Email
{
    public interface IOltSmtpConfiguration : IDisposable
    {
        string Server { get; }
        bool DisableSsl { get; }
        int Port { get; }
        string Username { get; }
        string Password { get; }
    }
}
using System;
using Microsoft.AspNetCore.Authentication;

namespace OLT.AspNetCore.Authentication
{
    public interface IOltAuthenticationScheme
    {
        string Scheme { get; }
    }

 
}
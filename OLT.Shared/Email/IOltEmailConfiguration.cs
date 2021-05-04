using System.Collections.Generic;
using OLT.Core;

// ReSharper disable once CheckNamespace
namespace OLT.Email
{
    public interface IOltEmailConfiguration : IOltInjectableScoped
    {
        OltEmailAddress From { get; }
        bool IsProduction { get; }
        List<string> DomainWhiteList { get; }
        List<string> EmailWhiteList { get; }
        bool SendEmail(string emailAddress);
    }

}
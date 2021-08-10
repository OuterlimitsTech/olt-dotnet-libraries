using System.Collections.Generic;
using OLT.Core;

// ReSharper disable once CheckNamespace
namespace OLT.Email
{

    public interface IOltEmailConfiguration : IOltInjectableScoped
    {
        OltEmailAddress From { get; }
        bool IsProduction { get; }
        OltEmailTestingWhitelist TestWhitelist { get; }
        bool SendEmail(string emailAddress);
    }

}
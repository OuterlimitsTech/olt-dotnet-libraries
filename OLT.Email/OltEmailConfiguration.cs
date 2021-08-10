using System;
using System.Collections.Generic;
using System.Linq;
using OLT.Core;

namespace OLT.Email
{
    public abstract class OltEmailConfiguration : OltDisposable, IOltEmailConfiguration
    {
        public abstract OltEmailAddress From { get; }
        public abstract bool IsProduction { get; }
        public abstract List<string> DomainWhiteList { get; }
        public abstract List<string> EmailWhiteList { get; }

        public virtual bool SendEmail(string emailAddress)
        {
            if (IsProduction)
            {
                return true;
            }
            
            return DomainWhiteList?.Any(p => emailAddress.EndsWith(p, StringComparison.OrdinalIgnoreCase)) == true ||
                   EmailWhiteList?.Any(p => emailAddress.Equals(p, StringComparison.OrdinalIgnoreCase)) == true;
        }

    }
}
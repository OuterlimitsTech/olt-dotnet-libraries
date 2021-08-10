using System;
using System.Collections.Generic;
using System.Linq;
using OLT.Core;

namespace OLT.Email
{
    public class OltEmailTestingWhitelist
    {
        public List<string> Domain { get; set; }
        public List<string> Email { get; set; }
    }

    public abstract class OltEmailConfiguration : OltDisposable, IOltEmailConfiguration
    {
        public abstract OltEmailAddress From { get; }
        public abstract bool IsProduction { get; }
        public virtual OltEmailTestingWhitelist TestWhitelist { get; set; } = new OltEmailTestingWhitelist();

        public virtual bool SendEmail(string emailAddress)
        {
            if (IsProduction)
            {
                return true;
            }
            
            return TestWhitelist?.Domain?.Any(p => emailAddress.EndsWith(p, StringComparison.OrdinalIgnoreCase)) == true ||
                   TestWhitelist?.Email?.Any(p => emailAddress.Equals(p, StringComparison.OrdinalIgnoreCase)) == true;
        }

    }
}
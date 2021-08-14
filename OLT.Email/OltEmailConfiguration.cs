using System;
using System.Linq;
using OLT.Core;

namespace OLT.Email
{
    public class OltEmailConfiguration : OltDisposable, IOltEmailConfiguration
    {
        public virtual OltEmailAddress From { get; set; } = new OltEmailAddress();

        /// <summary>
        /// Test Environment whitelist 
        /// </summary>
        public virtual OltEmailConfigurationWhitelist TestWhitelist { get; set; } = new OltEmailConfigurationWhitelist();

        public virtual bool Production { get; set; }

        public virtual bool SendEmail(string emailAddress)
        {
            if (Production)
            {
                return true;
            }
            
            return TestWhitelist?.Domain?.Any(p => emailAddress.EndsWith(p, StringComparison.OrdinalIgnoreCase)) == true ||
                   TestWhitelist?.Email?.Any(p => emailAddress.Equals(p, StringComparison.OrdinalIgnoreCase)) == true;
        }

    }
}
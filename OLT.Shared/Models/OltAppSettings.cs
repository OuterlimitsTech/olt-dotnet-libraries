using System;
using System.Collections.Generic;
using System.Text;

namespace OLT.Core
{   

    public class OltAppSettings : IOltAppSettings
    {
        /// <summary>
        /// Support email shown on exception responses. 
        /// </summary>
        /// <remarks>
        /// Default: support@outerlimitstech.com
        /// </remarks>
        public virtual string SupportEmail { get; set; } = "support@outerlimitstech.com";
    }

}

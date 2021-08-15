using System.Collections.Generic;

namespace OLT.Email
{

    /// <summary>
    /// This is for Test environments and should not be used in Production
    /// </summary>
    public class OltEmailConfigurationWhitelist
    {
        public List<string> Domain { get; set; } = new List<string>();
        public List<string> Email { get; set; } = new List<string>();
    }
}
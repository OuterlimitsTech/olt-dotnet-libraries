using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.Models
{
    // ReSharper disable once InconsistentNaming
    public class AppSettingsDto : OltAspNetAppSettings //<OltAuthenticationToken>
    {
        public string JwtSecret { get; set; }
    }
}
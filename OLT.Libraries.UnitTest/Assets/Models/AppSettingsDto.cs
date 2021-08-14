using OLT.Core;
using OLT.Email;

namespace OLT.Libraries.UnitTest.Assets.Models
{
    // ReSharper disable once InconsistentNaming
    public class AppSettingsDto : OltAspNetAppSettings
    {
        public string JwtSecret { get; set; }
        public OltAppSettingsSendGrid SendGrid { get; set; } = new OltAppSettingsSendGrid();
    }
}
using Microsoft.Extensions.Options;
using OLT.Email;
using OLT.Email.SendGrid;
using System.Collections.Generic;

namespace OLT.Libraries.UnitTest.Assets.Email.SendGrid
{
    public class EmailApiConfiguration : OltEmailConfiguration, IOltEmailConfigurationSendGrid
    {

        private readonly IOptions<OltSendGridAppSettings> _settings;

        public EmailApiConfiguration(IOptions<OltSendGridAppSettings> settings)
        {
            _settings = settings;
        }

        public override OltEmailAddress From => new OltEmailAddress
        {
            Name = _settings.Value.FromName ?? "OLT",
            Email = _settings.Value.FromEmail ?? "noreply@outerlimitstech.com"
        };

        public override bool IsProduction => _settings.Value.Production;
        
        //public override List<string> DomainWhiteList => _settings.Value.DomainWhitelist?.Split(';').ToList() ?? new List<string>();
        //public override List<string> EmailWhiteList => _settings.Value.EmailWhitelist?.Split(';').ToList() ?? new List<string>();
        public string ApiKey => _settings.Value.ApiKey;
        public bool ClickTracking => true;
    }

}
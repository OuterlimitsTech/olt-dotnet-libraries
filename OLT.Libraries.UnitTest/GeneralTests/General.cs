using Microsoft.Extensions.Options;
using OLT.Core;
using OLT.Libraries.UnitTest.Abstract;
using OLT.Libraries.UnitTest.Assets.Models;
using Xunit;
using Xunit.Abstractions;

namespace OLT.Libraries.UnitTest.GeneralTests
{
    public class General : BaseTest
    {
        private AppSettingsDto _appSettings;
        private readonly IOltConfigManager _configManager;

        public General(
            IOptions<AppSettingsDto> appOptions,
            IOltConfigManager configManager,
            ITestOutputHelper output) : base(output)
        {
            _configManager = configManager;
            _appSettings = appOptions.Value;
        }


        [Fact]
        public void JwtSecretAppSettings()
        {
            Assert.True(_appSettings?.JwtSecret?.Equals("JwtSecret-Test"));
        }

        [Fact]
        public void JwtSecretConfigManager()
        {
            var value = _configManager.GetValue<string>("AppSettings:JwtSecret");
            Assert.True(value?.Equals(_appSettings.JwtSecret));
        }
    }
}
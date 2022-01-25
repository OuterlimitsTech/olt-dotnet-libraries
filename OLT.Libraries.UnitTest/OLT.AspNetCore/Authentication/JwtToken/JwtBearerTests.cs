using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication.JwtToken
{

  

    public class JwtBearerTests
    {

        [Fact]
        public void ArgumentExceptions()
        {
            var services = new ServiceCollection();
            var invalidOptions = new OltAuthenticationJwtBearer();
            var validOptions = new OltAuthenticationJwtBearer(ApiKeyStartup.Secret);

            Action<JwtBearerOptions> action = (JwtBearerOptions opts) =>
            {                
                opts.RequireHttpsMetadata = false;
                opts.Authority = "local";
                opts.Audience = "local";
            };

            Action<AuthenticationOptions> authAction = (AuthenticationOptions opts) =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            };

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, validOptions));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null));


            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, validOptions, null));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null, action));

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, validOptions, action, authAction));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null, action, authAction));

            Assert.Throws<ArgumentNullException>("builder", () => invalidOptions.AddScheme(null));
            Assert.Throws<NullReferenceException>(() => invalidOptions.AddScheme(services.AddAuthentication(invalidOptions.Scheme)));

            Assert.Throws<ArgumentNullException>("services", () => invalidOptions.AddAuthentication(null));

        }

        private async Task AuthTest(TestServer testServer)
        {
            var options = JwtTokenTestExts.GetOptions();
            var services = testServer.Host.Services;
            var schemeProvider = services.GetRequiredService<IAuthenticationSchemeProvider>();
            Assert.NotNull(schemeProvider);            
            var scheme = await schemeProvider.GetDefaultAuthenticateSchemeAsync();
            Assert.NotNull(scheme);
            Assert.Equal(typeof(JwtBearerHandler), scheme.HandlerType);

            var optionsSnapshot = services.GetService<IOptionsSnapshot<JwtBearerOptions>>();
            var schemeOptions = optionsSnapshot.Get(scheme.Name);
            Assert.NotNull(schemeOptions);

            Assert.Equal(options.Scheme, scheme.Name);
            Assert.Equal(options.RequireHttpsMetadata, schemeOptions.RequireHttpsMetadata);
            Assert.Equal(options.ValidateIssuer, schemeOptions.TokenValidationParameters.ValidateIssuer);
            Assert.Equal(options.ValidateAudience, schemeOptions.TokenValidationParameters.ValidateAudience);
            Assert.Equal(JwtTokenTestExts.Authority, schemeOptions.Authority);
            Assert.Equal(JwtTokenTestExts.Audience, schemeOptions.Audience);
        }

        [Fact]
        public async Task ApiKeyStartupDefault()
        {
            using (var testServer = new TestServer(UnitTestHelper.WebHostBuilder<JwtTokenStartupDefault>()))
            {
                await AuthTest(testServer);
            }
        }
    }
}

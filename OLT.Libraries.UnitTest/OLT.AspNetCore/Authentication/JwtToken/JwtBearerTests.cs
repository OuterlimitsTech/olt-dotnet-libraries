using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
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
            Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationJwtExtensions.AddJwtBearer(services, validOptions, null));

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, validOptions, action, authAction));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null, action, authAction));

            Assert.Throws<ArgumentNullException>("builder", () => invalidOptions.AddScheme(null));
            Assert.Throws<ArgumentNullException>("JwtSecret", () => invalidOptions.AddScheme(services.AddAuthentication(invalidOptions.Scheme)));

            Assert.Throws<ArgumentNullException>("services", () => invalidOptions.AddAuthentication(null));

        }


        ////[Fact]
        ////public void AddAuthentication()
        ////{
        ////    var webBuilder = new WebHostBuilder();
        ////    webBuilder.UseStartup<JwtTokenStartup>();


        ////    //var services = new ServiceCollection();
        ////    //var options = new OltAuthenticationJwtBearer();
        ////    //Action<JwtBearerOptions> jwtAction = (JwtBearerOptions opts) =>
        ////    //{
        ////    //    opts.RequireHttpsMetadata = false;
        ////    //    opts.Authority = "local";
        ////    //    opts.Audience = "local";
        ////    //};

        ////    //Action<AuthenticationOptions> authAction = (AuthenticationOptions opts) =>
        ////    //{
        ////    //    opts.
        ////    //};

        ////    //options.Disabled = true;

        ////    ////OltAuthenticationJwtExtensions.AddAuthentication(services, options, authAction, jwtAction);


        ////    //options.Disabled = false;
        ////    //var builder = services.AddAuthentication(options);

        ////    //using (var testServer = new TestServer(webBuilder))
        ////    //{
        ////    //    var response = await testServer.CreateRequest("/api/league/2").SendAsync("GET");
        ////    //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        ////    //}


        ////    //Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, options));
        ////    //Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null));


        ////    //Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, options, null));
        ////    //Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null, action));
        ////    //Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationJwtExtensions.AddJwtBearer(services, options, null));


        ////    //Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddAuthentication(null, options));
        ////    //Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddAuthentication<OltAuthenticationJwtBearer>(services, null));

        ////    //Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddAuthentication(null, options, null));
        ////    //Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddAuthentication<OltAuthenticationJwtBearer>(services, null, action));
        ////    //Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationJwtExtensions.AddAuthentication(services, options, null));

        ////}
    }
}

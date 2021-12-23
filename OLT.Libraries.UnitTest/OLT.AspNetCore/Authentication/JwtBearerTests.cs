using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using OLT.AspNetCore.Authentication;
using OLT.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore.Authentication
{
    public class JwtBearerTests
    {

        [Fact]
        public void ArgumentExceptions()
        {
            var services = new ServiceCollection();
            var options = new OltAuthenticationJwtBearer();
            Action<JwtBearerOptions> action = (JwtBearerOptions opts) =>
            {
                opts.RequireHttpsMetadata = false;
                opts.Authority = "local";
                opts.Audience = "local";
            };

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, options));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null));


            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, options, null));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null, action));
            Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationJwtExtensions.AddJwtBearer(services, options, null));


            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddAuthentication(null, options));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddAuthentication<OltAuthenticationJwtBearer>(services, null));

            Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddAuthentication(null, options, null));
            Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddAuthentication<OltAuthenticationJwtBearer>(services, null, action));
            Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationJwtExtensions.AddAuthentication(services, options, null));

        }


        [Fact]
        public void AddAuthentication()
        {
            var services = new ServiceCollection();
            var options = new OltAuthenticationJwtBearer();
            Action<JwtBearerOptions> jwtAction = (JwtBearerOptions opts) =>
            {
                opts.RequireHttpsMetadata = false;
                opts.Authority = "local";
                opts.Audience = "local";
            };

            //Action<AuthenticationOptions> authAction = (AuthenticationOptions opts) =>
            //{
            //    opts.
            //};

            options.Disabled = true;

            //OltAuthenticationJwtExtensions.AddAuthentication(services, options, authAction, jwtAction);


            options.Disabled = false;


            //Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, options));
            //Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null));


            //Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddJwtBearer(null, options, null));
            //Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddJwtBearer<OltAuthenticationJwtBearer>(services, null, action));
            //Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationJwtExtensions.AddJwtBearer(services, options, null));


            //Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddAuthentication(null, options));
            //Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddAuthentication<OltAuthenticationJwtBearer>(services, null));

            //Assert.Throws<ArgumentNullException>("services", () => OltAuthenticationJwtExtensions.AddAuthentication(null, options, null));
            //Assert.Throws<ArgumentNullException>("options", () => OltAuthenticationJwtExtensions.AddAuthentication<OltAuthenticationJwtBearer>(services, null, action));
            //Assert.Throws<ArgumentNullException>("configureOptions", () => OltAuthenticationJwtExtensions.AddAuthentication(services, options, null));

        }
    }
}

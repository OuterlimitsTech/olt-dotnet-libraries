using OLT.Core;
using OLT.Libraries.UnitTest.Assets.LocalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OLT.Libraries.UnitTest.OLT.Shared
{
    public class OltIdentityTests
    {
        [Fact]
        public void ClaimsTest()
        {
            var fakeUser = new FakeUserIdentity();
            var identity = new UnitTestingAppIdentity(fakeUser);

            Assert.Equal(fakeUser.UserPrincipalName.ToString(), identity.GetClaimTypes(ClaimTypes.Upn)?[0]?.Value);
            Assert.Equal(fakeUser.NameIdentifier, identity.GetClaimTypes(ClaimTypes.NameIdentifier)?[0]?.Value);
            Assert.Equal(fakeUser.PersonName.First, identity.GetClaimTypes(ClaimTypes.GivenName)?[0]?.Value);
            Assert.Equal(fakeUser.PersonName.Middle, identity.GetClaimTypes(OltClaimTypes.MiddleName)?[0]?.Value);
            Assert.Equal(fakeUser.PersonName.Last, identity.GetClaimTypes(ClaimTypes.Surname)?[0]?.Value);
            Assert.Equal(fakeUser.Email, identity.GetClaimTypes(ClaimTypes.Email)?[0]?.Value);
            Assert.Equal(fakeUser.Phone, identity.GetClaimTypes(ClaimTypes.HomePhone)?[0]?.Value);
            Assert.Equal(fakeUser.Claims.FirstOrDefault(p => p.Type == ClaimTypes.MobilePhone)?.Value, identity.GetClaimTypes(ClaimTypes.MobilePhone)?[0]?.Value);

        }
    }
}

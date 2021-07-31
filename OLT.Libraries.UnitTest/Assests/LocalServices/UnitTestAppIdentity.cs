using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assests.LocalServices
{
    public class OltUnitTestAppIdentity : OltIdentity
    {
        private const string StaticUser = "UnitTest";

        public override ClaimsPrincipal Identity
        {
            get
            {
                var roles = new List<string>();
                //foreach (SecurityRoles role in Enum.GetValues(typeof(SecurityRoles)))
                //{
                //    roles.Add(role.GetCodeEnum());
                //}

                //foreach (SecurityPermissions perm in Enum.GetValues(typeof(SecurityPermissions)))
                //{
                //    roles.Add(perm.GetCodeEnum());
                //}

                return new GenericPrincipal(new GenericIdentity(StaticUser), roles.ToArray());
            }
        }

        public override string Username => StaticUser;
        public override string UserPrincipalName => Email;
        public override string Email => "developers@outerlimitstech.com";

        public override bool HasRoleClaim(string claimName)
        {
            return true;
        }

    }
}
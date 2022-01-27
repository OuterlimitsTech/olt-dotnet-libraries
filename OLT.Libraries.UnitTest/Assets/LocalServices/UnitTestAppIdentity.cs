using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using OLT.Core;

namespace OLT.Libraries.UnitTest.Assets.LocalServices
{
    public class OltUnitTestNullIdentity : OltIdentity
    {

        public override ClaimsPrincipal Identity
        {
            get
            {
                var roles = new List<string>();
                return new GenericPrincipal(new GenericIdentity("bogus"), roles.ToArray());
            }
        }

        public override string Username => null;
        public override string UserPrincipalName => null;
        public override string Email => null;

        public override bool HasRoleClaim(string claimName)
        {
            return true;
        }

    }

    public class OltUnitTestAppIdentity : OltIdentity
    {
        public const string StaticUser = "UnitTest";
        public const string StaticEmail = "developers@outerlimitstech.com";

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
        public override string Email => StaticEmail;

        public override bool HasRoleClaim(string claimName)
        {
            return true;
        }

    }

    public enum TestSecurityRoles
    {
        [Code("role-one")]
        RoleOne,

        [Code("role-two")]
        RoleTwo,

        [Code("role-three")]
        RoleThree,
    }

    public enum TestSecurityPermissions
    {
        [Code("perm-one")]
        PermissionOne,

        [Code("perm-two")]
        PermissionTwo,

        [Code("perm-three")]
        PermissionThree,
    }


    public class FakeUserIdentity
    {

        public FakeUserIdentity() 
        {
            NameIdentifier = Faker.Internet.UserName();
            Email = Faker.Internet.Email();
            UserPrincipalName = Faker.RandomNumber.Next(1000, 9000);
            Phone = Faker.Phone.Number();
            MobilePhone = Faker.Phone.Number();

            PersonName = new OltPersonName
            {
                First = Faker.Name.First(),
                Middle = Faker.Name.Middle(),
                Last = Faker.Name.Last(),
            };
        }
        private List<Claim> _claims = new List<Claim>();
        public string NameIdentifier { get; }
        public string Email {  get; }
        public int UserPrincipalName { get; }
        public string Phone { get; }
        public string MobilePhone { get; }
        public OltPersonName PersonName { get; }

        public List<TestSecurityRoles> Roles => new List<TestSecurityRoles> { TestSecurityRoles.RoleOne, TestSecurityRoles.RoleTwo };
        public List<TestSecurityPermissions> Permissions => new List<TestSecurityPermissions> { TestSecurityPermissions.PermissionOne, TestSecurityPermissions.PermissionThree };

        public List<Claim> Claims
        {
            get
            {
                if (_claims.Any())
                {
                    return _claims;
                }

                _claims.Add(new Claim(ClaimTypes.Upn, UserPrincipalName.ToString()));
                _claims.Add(new Claim(ClaimTypes.NameIdentifier, NameIdentifier));
                _claims.Add(new Claim(ClaimTypes.GivenName, PersonName.First));
                _claims.Add(new Claim(OltClaimTypes.MiddleName, PersonName.Middle));
                _claims.Add(new Claim(ClaimTypes.Surname, PersonName.Last));
                _claims.Add(new Claim(ClaimTypes.Name, PersonName.FullName));
                _claims.Add(new Claim(ClaimTypes.Email, Email));
                _claims.Add(new Claim(ClaimTypes.HomePhone, Phone));
                _claims.Add(new Claim(ClaimTypes.MobilePhone, MobilePhone));
                return _claims;
            }
        }
    }

    public class UnitTestingAppIdentity : OltIdentity
    {
        public UnitTestingAppIdentity(FakeUserIdentity fakeUser)
        {
            FakeUser = fakeUser;
        }

        public FakeUserIdentity FakeUser { get; }


        public override ClaimsPrincipal Identity
        {
            get
            {
                //var roles = new List<string>();
                var identity = new GenericIdentity(FakeUser.UserPrincipalName.ToString());
                FakeUser.Claims.ForEach(claim => identity.AddClaim(claim));

                FakeUser.Roles.ForEach(role =>
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.GetCodeEnum()));
                    //roles.Add(role.GetCodeEnum());
                });
                FakeUser.Permissions.ForEach(perm =>
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, perm.GetCodeEnum()));
                    //roles.Add(perm.GetCodeEnum())
                });

                return new GenericPrincipal(identity, null);
            }
        }
       
    }
}
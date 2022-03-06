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
                return new GenericPrincipal(new GenericIdentity(null), roles.ToArray());
            }
        }

        public override string Username => null;
        public override string UserPrincipalName => null;
        public override string Email => null;

        public override bool HasRole(string claimName)
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

        public override bool HasRole(string claimName)
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

        [Code("role-four")]
        RoleFour,

    }

    public enum TestSecurityPermissions
    {
        [Code("perm-one")]
        PermissionOne,

        [Code("perm-two")]
        PermissionTwo,

        [Code("perm-three")]
        PermissionThree,


        [Code("perm-four")]
        PermissionFour,
    }


    public class FakeUserIdentity
    {

        public FakeUserIdentity(bool loadFakeData) 
        {
            if (!loadFakeData)
            {
                return;
            }

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

            Roles = new List<TestSecurityRoles> { TestSecurityRoles.RoleOne, TestSecurityRoles.RoleTwo };
            Permissions = new List<TestSecurityPermissions> { TestSecurityPermissions.PermissionOne, TestSecurityPermissions.PermissionThree };
        }
        private List<Claim> _claims = new List<Claim>();
        public string NameIdentifier { get; }
        public string Email {  get; }
        public int UserPrincipalName { get; }
        public string Phone { get; }
        public string MobilePhone { get; }
        public OltPersonName PersonName { get; }


        public List<TestSecurityRoles> Roles { get; }
        public List<TestSecurityPermissions> Permissions { get; }

        public List<Claim> Claims
        {
            get
            {
                if (_claims.Any())
                {
                    return _claims;
                }

                if (UserPrincipalName > 0)
                {
                    AddClaim(ClaimTypes.Upn, UserPrincipalName.ToString());
                }
                
                AddClaim(ClaimTypes.NameIdentifier, NameIdentifier);
                AddClaim(ClaimTypes.GivenName, PersonName?.First);
                AddClaim(OltClaimTypes.MiddleName, PersonName?.Middle);
                AddClaim(ClaimTypes.Surname, PersonName?.Last);
                //AddClaim(ClaimTypes.Name, PersonName.FullName)); //Added by Generic Identity
                AddClaim(ClaimTypes.Email, Email);
                AddClaim(ClaimTypes.HomePhone, Phone);
                AddClaim(ClaimTypes.MobilePhone, MobilePhone);
                return _claims;
            }
        }

        private void AddClaim(string type, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _claims.Add(new Claim(type, value));
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
                if (FakeUser == null)
                {
                    return null;
                }

                //var roles = new List<string>();
                var identity = new GenericIdentity(FakeUser.PersonName?.FullName ?? "unknown");

                FakeUser.Claims.ForEach(claim => identity.AddClaim(claim));

                FakeUser.Roles?.ForEach(role =>
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.GetCodeEnum()));
                    //roles.Add(role.GetCodeEnum());
                });
                FakeUser.Permissions?.ForEach(perm =>
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, perm.GetCodeEnum()));
                    //roles.Add(perm.GetCodeEnum())
                });

                return new GenericPrincipal(identity, null);
            }
        }
       
    }
}
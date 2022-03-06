//using OLT.Core;
//using OLT.Libraries.UnitTest.Assets.LocalServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace OLT.Libraries.UnitTest.OLT.Shared
//{
//    public class OltIdentityTests
//    {
//        [Fact]
//        public void ClaimsTest()
//        {
//            var fakeUser = new FakeUserIdentity(true);
//            var identity = new UnitTestingAppIdentity(fakeUser);

//            Assert.NotNull(identity.Identity);
//            Assert.NotEmpty(identity.GetAllClaimTypes());
//            Assert.NotEmpty(identity.GetRoleClaims());
//            Assert.False(identity.IsAnonymous);            
//            Assert.Single(identity.GetClaimTypes(ClaimTypes.Upn));
//            Assert.Single(identity.GetClaimTypes(ClaimTypes.NameIdentifier));
//            Assert.Single(identity.GetClaimTypes(ClaimTypes.GivenName));
//            Assert.Single(identity.GetClaimTypes(OltClaimTypes.MiddleName));
//            Assert.Single(identity.GetClaimTypes(ClaimTypes.Surname));
//            Assert.Single(identity.GetClaimTypes(ClaimTypes.Name));
//            Assert.Single(identity.GetClaimTypes(ClaimTypes.Email));
//            Assert.Single(identity.GetClaimTypes(ClaimTypes.HomePhone));


//            Assert.Equal(fakeUser.UserPrincipalName.ToString(), identity.GetClaimTypes(ClaimTypes.Upn)?[0]?.Value);
//            Assert.Equal(fakeUser.NameIdentifier, identity.GetClaimTypes(ClaimTypes.NameIdentifier)?[0]?.Value);
//            Assert.Equal(fakeUser.PersonName.First, identity.GetClaimTypes(ClaimTypes.GivenName)?[0]?.Value);
//            Assert.Equal(fakeUser.PersonName.Middle, identity.GetClaimTypes(OltClaimTypes.MiddleName)?[0]?.Value);
//            Assert.Equal(fakeUser.PersonName.Last, identity.GetClaimTypes(ClaimTypes.Surname)?[0]?.Value);
//            Assert.Equal(fakeUser.PersonName.FullName, identity.GetClaimTypes(ClaimTypes.Name)?[0]?.Value);
//            Assert.Equal(fakeUser.Email, identity.GetClaimTypes(ClaimTypes.Email)?[0]?.Value);
//            Assert.Equal(fakeUser.Phone, identity.GetClaimTypes(ClaimTypes.HomePhone)?[0]?.Value);
//            Assert.Equal(fakeUser.Claims.FirstOrDefault(p => p.Type == ClaimTypes.MobilePhone)?.Value, identity.GetClaimTypes(ClaimTypes.MobilePhone)?[0]?.Value);

//            Assert.Equal(fakeUser.UserPrincipalName.ToString(), identity.UserPrincipalName);
//            Assert.Equal(fakeUser.NameIdentifier, identity.Username);
//            Assert.Equal(fakeUser.PersonName.First, identity.FirstName);
//            Assert.Equal(fakeUser.PersonName.Middle, identity.MiddleName);
//            Assert.Equal(fakeUser.PersonName.Last, identity.LastName);
//            Assert.Equal(fakeUser.Email, identity.Email);
//            Assert.Equal(fakeUser.Phone, identity.Phone);
//            Assert.Equal(fakeUser.PersonName.FullName, identity.FullName);
//            Assert.Equal(fakeUser.NameIdentifier, identity.GetDbUsername());

//            Assert.True(identity.HasRole(TestSecurityRoles.RoleOne));
//            Assert.True(identity.HasRole(TestSecurityRoles.RoleTwo));
//            Assert.False(identity.HasRole(TestSecurityRoles.RoleThree));
//            Assert.True(identity.HasRole(TestSecurityRoles.RoleThree, TestSecurityRoles.RoleTwo));
//            Assert.True(identity.HasRole(TestSecurityRoles.RoleThree, TestSecurityRoles.RoleOne));
//            Assert.True(identity.HasRole(TestSecurityRoles.RoleTwo, TestSecurityRoles.RoleOne));
//            Assert.False(identity.HasRole(TestSecurityRoles.RoleThree, TestSecurityRoles.RoleFour));

//            Assert.False(identity.HasRole<TestSecurityRoles>(null));


//            Assert.True(identity.HasPermission(TestSecurityPermissions.PermissionOne));
//            Assert.False(identity.HasPermission(TestSecurityPermissions.PermissionTwo));
//            Assert.True(identity.HasPermission(TestSecurityPermissions.PermissionThree));
//            Assert.False(identity.HasPermission(TestSecurityPermissions.PermissionFour));
//            Assert.True(identity.HasPermission(TestSecurityPermissions.PermissionThree, TestSecurityPermissions.PermissionTwo));
//            Assert.True(identity.HasPermission(TestSecurityPermissions.PermissionThree, TestSecurityPermissions.PermissionOne));
//            Assert.True(identity.HasPermission(TestSecurityPermissions.PermissionTwo, TestSecurityPermissions.PermissionOne));
//            Assert.True(identity.HasPermission(TestSecurityPermissions.PermissionThree, TestSecurityPermissions.PermissionFour));
//            Assert.False(identity.HasPermission(TestSecurityPermissions.PermissionTwo, TestSecurityPermissions.PermissionFour));

//            Assert.False(identity.HasPermission<TestSecurityPermissions>(null));

//            Assert.False(identity.HasRoleClaim(null));
//            Assert.False(identity.HasRoleClaim(Faker.Lorem.GetFirstWord()));
//            Assert.True(identity.HasRoleClaim(TestSecurityRoles.RoleOne.GetCodeEnum().ToLower()));
//            Assert.False(identity.HasRoleClaim(TestSecurityRoles.RoleThree.GetCodeEnum().ToLower()));

//            Assert.True(identity.HasRoleClaim(TestSecurityRoles.RoleOne.GetCodeEnum().ToUpper()));
//            Assert.False(identity.HasRoleClaim(TestSecurityRoles.RoleThree.GetCodeEnum().ToUpper()));
//        }


//        [Fact]
//        public void NullIdentity()
//        {
//            var identity = new UnitTestingAppIdentity(null);
//            Assert.Null(identity.Identity);
//            Assert.Empty(identity.GetAllClaimTypes());
//            Assert.Empty(identity.GetRoleClaims());
//            Assert.Empty(identity.GetClaimTypes(ClaimTypes.Name));
//            Assert.Empty(identity.GetClaimTypes(Faker.Lorem.GetFirstWord()));
//            Assert.True(identity.IsAnonymous);

//            Assert.Null(identity.UserPrincipalName);
//            Assert.Null(identity.Username);
//            Assert.Null(identity.FirstName);
//            Assert.Null(identity.MiddleName);
//            Assert.Null(identity.LastName);
//            Assert.Null(identity.Email);
//            Assert.Null(identity.Phone);
//            Assert.Null(identity.FullName);
//            Assert.Null(identity.GetDbUsername());

//            Assert.False(identity.HasRole(TestSecurityRoles.RoleThree));
//            Assert.False(identity.HasPermission(TestSecurityPermissions.PermissionOne));

//            Assert.False(identity.HasRoleClaim(null));
//            Assert.False(identity.HasRoleClaim(Faker.Lorem.GetFirstWord()));

//        }

//        [Fact]
//        public void NullUser()
//        {
//            var fakeUser = new FakeUserIdentity(false);
//            var identity = new UnitTestingAppIdentity(fakeUser);

//            Assert.NotNull(identity.Identity);
//            Assert.Single(identity.GetAllClaimTypes());
//            Assert.Empty(identity.GetRoleClaims());
//            Assert.Equal("unknown", identity.FullName);
//            Assert.Empty(identity.GetClaimTypes(Faker.Lorem.GetFirstWord()));
//            Assert.True(identity.IsAnonymous);
//            Assert.False(identity.HasRole(TestSecurityRoles.RoleThree));
//            Assert.False(identity.HasPermission(TestSecurityPermissions.PermissionOne));
//            Assert.False(identity.HasRoleClaim(null));
//            Assert.False(identity.HasRoleClaim(Faker.Lorem.GetFirstWord()));


//        }
//    }
//}

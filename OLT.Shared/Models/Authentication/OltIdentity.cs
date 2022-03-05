////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Security.Claims;

////namespace OLT.Core
////{
////    /// <summary>
////    /// Open Id Authorized User Interface to be injected using DI to provide current user info
////    /// </summary>
////    public abstract class OltIdentity : OltDisposable, IOltIdentity
////    {

////        /// <summary>
////        /// Is Anonymous Request (other properties will likely be null)
////        /// </summary>
////        public virtual bool IsAnonymous => Identity == null || Username == null;
////        public abstract ClaimsPrincipal Identity { get; }

////        public virtual string Username => GetClaimTypes(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
////        public virtual string FirstName => GetClaimTypes(ClaimTypes.GivenName).FirstOrDefault()?.Value;
////        public virtual string MiddleName => GetClaimTypes(OltClaimTypes.MiddleName).FirstOrDefault()?.Value;
////        public virtual string LastName => GetClaimTypes(ClaimTypes.Surname).FirstOrDefault()?.Value;
////        public virtual string Email => GetClaimTypes(ClaimTypes.Email).FirstOrDefault()?.Value;
////        public virtual string Phone => GetClaimTypes(ClaimTypes.HomePhone).FirstOrDefault()?.Value;
////        public virtual string FullName => GetClaimTypes(ClaimTypes.Name).FirstOrDefault()?.Value;
////        public virtual string UserPrincipalName => GetClaimTypes(ClaimTypes.Upn).FirstOrDefault()?.Value;

////        /// <summary>
////        /// Get All Claims in Token
////        /// </summary>
////        public virtual List<Claim> GetAllClaimTypes()
////        {
////            if (Identity != null)
////            {
////                return Identity.Claims.ToList();
////            }
////            return new List<Claim>();
////        }


////        /// <summary>
////        /// Get Claims by Type (System.Security.Claims.ClaimTypes)
////        /// </summary>
////        public virtual List<Claim> GetClaimTypes(string type)
////        {
////            return GetAllClaimTypes().Where(p => p.Type == type).ToList();
////        }
////        /// <summary>
////        /// Gets All Role Claims
////        /// </summary>
////        public virtual List<Claim> GetRoleClaims()
////        {
////            return GetAllClaimTypes().Where(p => p.Type == ClaimTypes.Role).ToList();
////        }

////        /// <summary>
////        /// Checks if token has role claim
////        /// </summary>
////        public virtual bool HasRoleClaim(string claimName)
////        {
////            return GetRoleClaims().Any(p => string.Equals(p.Value, claimName, StringComparison.OrdinalIgnoreCase));
////        }

////        /// <summary>
////        /// Returns Username
////        /// </summary>
////        public virtual string GetDbUsername()
////        {
////            return this.Username;
////        }

////        public bool HasRole<TRoleEnum>(params TRoleEnum[] roles) where TRoleEnum : System.Enum
////        {
////            if (roles == null) return false;

////            var val = false;
////            foreach (var role in roles)
////            {
////                if (HasRoleClaim(role.GetCodeEnum()))
////                {
////                    val = true;
////                    break;
////                }
////            }
////            return val;
////        }

////        public bool HasPermission<TPermissionEnum>(params TPermissionEnum[] permissions) where TPermissionEnum : System.Enum
////        {
////            return HasRole(permissions);
////        }

////    }
////}
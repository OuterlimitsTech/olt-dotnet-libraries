////using System.Collections.Generic;
////using System.Security.Claims;

////namespace OLT.Core
////{
////    /// <summary>
////    /// Open Id Authorized User Interface to be injected using DI to provide current user info
////    /// </summary>
////    public interface IOltIdentity : IOltDbAuditUser, IOltIdentityUser
////    {
////        /// <summary>
////        /// Is Anonymous Request (other properties will likely be null)
////        /// </summary>
////        bool IsAnonymous { get; }

////        /// <summary>
////        /// Get Claims by Type (System.Security.Claims.ClaimTypes)
////        /// </summary>
////        List<Claim> GetClaimTypes(string type);

////        /// <summary>
////        /// Get All Claims in Token
////        /// </summary>
////        List<Claim> GetAllClaimTypes();

////        /// <summary>
////        /// Gets All Role Claims
////        /// </summary>
////        List<Claim> GetRoleClaims();

////        /// <summary>
////        /// Checks if token has role claim
////        /// </summary>
////        bool HasRoleClaim(string claimName);

////        bool HasRole<TRoleEnum>(params TRoleEnum[] roles) where TRoleEnum : System.Enum;

////        bool HasPermission<TPermissionEnum>(params TPermissionEnum[] permissions) where TPermissionEnum : System.Enum;

////    }
////}
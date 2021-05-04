using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace OLT.Core
{
    /// <summary>
    /// Open Id Authorized User Interface to be injected using DI to provide current user info
    /// </summary>
    [Obsolete("Move to OltIdentity")]
    public abstract class OltAuthBearerTokenUser : OltDisposable, IOltAuthBearerTokenUser
    {

        /// <summary>
        /// Is Anonymous Request (other properties will likely be null)
        /// </summary>
        public virtual bool IsAnonymous => Identity == null || GetUsername() == null;


        public abstract ClaimsPrincipal Identity { get; }

        /// <summary>
        /// Get All Claims in Token
        /// </summary>
        public virtual List<Claim> GetAllClaimTypes()
        {
            if (Identity != null)
            {
                return Identity.Claims.ToList();
            }
            return new List<Claim>();
        }

        /// <summary>
        /// Get Claims by Type (System.Security.Claims.ClaimTypes)
        /// </summary>
        public virtual List<Claim> GetClaimTypes(string type)
        {
            return GetAllClaimTypes().Where(p => p.Type == type).ToList();
        }

        /// <summary>
        /// Gets Claim Type ClaimTypes.Name
        /// Typically the User's Full Name from the OAuth provider
        /// </summary>
        public virtual string GetFriendlyName()
        {
            var claimUser = GetAllClaimTypes().FirstOrDefault(a => a.Type == ClaimTypes.Name);
            return claimUser?.Value;
        }

        /// <summary>
        /// Gets Claim Type ClaimTypes.NameIdentifier (Username)
        /// Typically the Unique Id from the OAuth Provider
        /// Recommended to retrieve the user from User Store
        /// </summary>
        public virtual string GetUsername()
        {
            var claimUser = GetAllClaimTypes().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier);
            return claimUser?.Value;
        }

        /// <summary>
        /// Gets All Role Claims
        /// </summary>
        public virtual List<Claim> GetRoleClaims()
        {
            return GetAllClaimTypes().Where(p => p.Type == ClaimTypes.Role).ToList();
        }

        /// <summary>
        /// Gets Claim Type ClaimTypes.Upn
        /// If provided when generating the bearer token, this will be the system provided user identifier
        /// </summary>
        public virtual string GetUserPrincipalName()
        {
            var claimUser = GetAllClaimTypes().FirstOrDefault(a => a.Type == ClaimTypes.Upn);
            return claimUser?.Value;
        }

        /// <summary>
        /// Checks if token has role claim
        /// </summary>
        public virtual bool HasRoleClaim(string claimName)
        {
            return GetRoleClaims()?.Any(p => string.Equals(p.Value, claimName, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        public virtual string GetDbUsername()
        {
            return GetUsername();
        }
    }
}
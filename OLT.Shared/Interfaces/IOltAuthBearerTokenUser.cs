using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace OLT.Core
{
    /// <summary>
    /// Open Id Authorized User Interface to be injected using DI to provide current user info
    /// </summary>
    [Obsolete("Move to IOltIdentity")]
    public interface IOltAuthBearerTokenUser : IOltDbAuditUser
    {
        /// <summary>
        /// Is Anonymous Request (other properties will likely be null)
        /// </summary>
        bool IsAnonymous { get; }


        /// <summary>
        /// Gets Claim Type ClaimTypes.Upn
        /// If provided when generating the bearer token, this will be the system provided user identifier
        /// </summary>
        string GetUserPrincipalName();

        /// <summary>
        /// Gets Claim Type ClaimTypes.NameIdentifier (Username)
        /// Typically the Unique Id from the OAuth Provider
        /// Recommended to retrieve the user from User Store
        /// </summary>
        string GetUsername();

        /// <summary>
        /// Gets Claim Type ClaimTypes.Name
        /// Typically the User's Full Name from the OAuth provider
        /// </summary>
        string GetFriendlyName();

        /// <summary>
        /// Get Claims by Type (System.Security.Claims.ClaimTypes)
        /// </summary>
        List<Claim> GetClaimTypes(string type);

        /// <summary>
        /// Get All Claims in Token
        /// </summary>
        List<Claim> GetAllClaimTypes();

        /// <summary>
        /// Gets All Role Claims
        /// </summary>
        List<Claim> GetRoleClaims();

        /// <summary>
        /// Checks if token has role claim
        /// </summary>
        bool HasRoleClaim(string claimName);

    }
}
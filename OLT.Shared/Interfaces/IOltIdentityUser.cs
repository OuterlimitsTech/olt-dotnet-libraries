namespace OLT.Core
{
    public interface IOltIdentityUser
    {
        /// <summary>
        /// Claim Type ClaimTypes.Upn
        /// To Be used for local system Id
        /// </summary>
        string UserPrincipalName { get; }

        /// <summary>
        /// THIS PROPERTY IS REQUIRED!!!
        /// Unique Id from the OAuth Provider
        /// or
        /// Username provided from Token of User
        /// </summary>
        /// <value>ClaimTypes.NameIdentifier</value>
        string Username { get; }

        /// <summary>
        /// First Name
        /// </summary>
        /// <value>ClaimTypes.GivenName</value>
        string FirstName { get; }

        /// <summary>
        /// Email Address of User
        /// </summary>
        /// <value>AppDevClaimTypes.MiddleName</value>
        string MiddleName { get; }

        /// <summary>
        /// Email Address of User
        /// </summary>
        /// <value>ClaimTypes.Surname</value>
        string LastName { get; }


        /// <summary>
        /// Email Address of User
        /// </summary>
        /// <value>ClaimTypes.Email</value>
        string Email { get; }


        /// <summary>
        /// Full Name of User
        /// </summary>
        /// <value>ClaimTypes.Name</value>
        string FullName { get; }
    }
}
using System;
using System.Collections.Generic;

namespace OLT.Core
{
    public abstract class OltAuthenticatedUserJson<TNameModel> 
        where TNameModel : class, IOltPersonName, new()
    {
        public virtual int UserPrincipalName { get; set; }
        public virtual string Username { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string FullName => Name.FullName;
        public virtual TNameModel Name { get; set; } = new TNameModel();

        public virtual string AuthenticationType { get; set; } = OltDefaults.Authentication.Jwt.AuthenticationScheme;
        public virtual string Token { get; set; }
        public virtual DateTimeOffset Issued { get; set; }
        public virtual DateTimeOffset Expires { get; set; }
        public virtual string ExpiresIn => $"{(Expires - Issued).TotalSeconds}";

        public virtual IEnumerable<string> Roles { get; set; }
        public virtual IEnumerable<string> Permissions { get; set; }

    }
}
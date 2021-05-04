using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OLT.Core
{

    public class OltAuthenticatedUserJson<TNameModel> 
        where TNameModel : class, IOltPersonName, new()
    {
        public virtual int UserPrincipalName { get; set; }
        public virtual string Username { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string FullName => Name.FullName;
        public virtual TNameModel Name { get; set; } = new TNameModel();

        [JsonProperty("token_type")]
        public virtual string AuthenticationType { get; set; } = OltDefaults.JwtAuthenticationScheme;
        [JsonProperty("access_token")]
        public virtual string Token { get; set; }
        public virtual DateTimeOffset Issued { get; set; }
        public virtual DateTimeOffset Expires { get; set; }
        [JsonProperty("expires_in")]
        public virtual string ExpiresIn => $"{(Expires - Issued).TotalSeconds}";

        public virtual IEnumerable<string> Roles { get; set; }
        public virtual IEnumerable<string> Permissions { get; set; }

    }
}
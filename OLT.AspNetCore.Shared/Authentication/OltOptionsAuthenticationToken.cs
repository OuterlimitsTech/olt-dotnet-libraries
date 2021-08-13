////namespace OLT.Core
////{
////    public class OltOptionsAuthenticationToken 
////    {

////        public string JwtSecret { get; set; }
////        //public override string DefaultAuthenticateScheme { get; set; } = OltDefaults.JwtAuthenticationScheme;
////        //public override string DefaultChallengeScheme { get; set; } = OltDefaults.JwtAuthenticationScheme;

////        /// <summary>
////        /// Gets or sets if HTTPS is required for the metadata address or authority.
////        /// </summary>
////        /// <remarks>
////        /// The default is true. This should be disabled only in development environments.
////        /// </remarks>
////        public bool RequireHttpsMetadata { get; set; } = true;


////        /// <summary>
////        /// Gets or sets a boolean to control if the audience will be validated during token validation.
////        /// </summary>
////        /// <remarks>
////        ///  Validation of the audience, mitigates forwarding attacks. For example, a site
////        ///  that receives a token, could not replay it to another side. A forwarded token
////        ///  would contain the audience of the original site. This boolean only applies to
////        ///  default audience validation. If Microsoft.IdentityModel.Tokens.TokenValidationParameters.AudienceValidator
////        ///  is set, it will be called regardless of whether this property is true or false.
////        /// </remarks>
////        public bool ValidateIssuer { get; set; }

////        /// <summary>
////        /// Gets or sets a boolean to control if the original token should be saved after the security token is validated.
////        /// </summary>
////        /// <remarks>
////        /// The runtime will consult this value and save the original token that was validated.
////        /// </remarks>
////        public bool ValidateAudience { get; set; }
////    }
////}
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OLT.Core;

namespace OLT.AspNetCore.Authentication
{
    public class OltAuthenticationJwtBearer : OltAuthenticationSchemeBuilder<JwtBearerOptions>, IOltAuthenticationJwtBearer
    {
        public OltAuthenticationJwtBearer()
        {

        }

        public OltAuthenticationJwtBearer(string jwtSecret)
        {
            JwtSecret = jwtSecret;
        }

        /// <summary>
        /// Default <seealso cref="JwtBearerDefaults.AuthenticationScheme"/>
        /// </summary>
        public override string Scheme => JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// Secret used to encrypt/decrypt jwt signature <seealso cref="TokenValidationParameters.IssuerSigningKey"/>
        /// </summary>
        public string JwtSecret { get; set; }

        /// <summary>
        /// Gets or sets if HTTPS is required for the metadata address or authority.
        /// </summary>
        /// <remarks>
        /// The default is true. This should be disabled only in development environments.
        /// </remarks>
        public bool RequireHttpsMetadata { get; set; } = true;


        /// <summary>
        /// Gets or sets a boolean to control if the audience will be validated during token validation.
        /// </summary>
        /// <remarks>
        ///  Validation of the audience, mitigates forwarding attacks. For example, a site
        ///  that receives a token, could not replay it to another side. A forwarded token
        ///  would contain the audience of the original site. This boolean only applies to
        ///  default audience validation. If Microsoft.IdentityModel.Tokens.TokenValidationParameters.AudienceValidator
        ///  is set, it will be called regardless of whether this property is true or false.
        /// </remarks>
        public bool ValidateIssuer { get; set; }

        /// <summary>
        /// Gets or sets a boolean to control if the original token should be saved after the security token is validated.
        /// </summary>
        /// <remarks>
        /// The runtime will consult this value and save the original token that was validated.
        /// </remarks>
        public bool ValidateAudience { get; set; }


        /// <summary>
        /// Builds Jwt Bearer Token Authentication Scheme
        /// </summary>
        /// <param name="builder"><seealso cref="AuthenticationBuilder"/></param>
        /// <param name="configureOptions"><seealso cref="JwtBearerOptions"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public override AuthenticationBuilder AddScheme(AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(JwtSecret))
            {
                throw new OltException(nameof(JwtSecret));
            }

            var key = Encoding.ASCII.GetBytes(JwtSecret);

            builder.AddJwtBearer(opt =>
            {

#pragma warning disable S125
                //opt.Events = new JwtBearerEvents
                //{
                //    OnTokenValidated = context =>
                //    {
                //        //var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                //        //var userId = int.Parse(context.Principal.Identity.Name);
                //        //if (context.Principal.Identity is ClaimsIdentity identity)
                //        //{
                //        //    var userId = Convert.ToInt32(identity.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Upn)?.Value);
                //        //}

                //        //var user = userService.GetById(userId);
                //        //if (user == null)
                //        //{
                //        //    // return unauthorized if user no longer exists
                //        //    context.Fail("Unauthorized");
                //        //}
                //        return Task.CompletedTask;
                //    }
                //};
#pragma warning restore S125
                opt.RequireHttpsMetadata = RequireHttpsMetadata;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = ValidateIssuer,
                    ValidateAudience = ValidateAudience
                };

                configureOptions?.Invoke(opt);
            });

            return builder;
        }


    }
}
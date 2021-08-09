using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OLT.Core
{

    public abstract class OltRequirePermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {

        public abstract List<string> RoleClaims { get; }


        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var principal = context.HttpContext.User;

            if (principal?.Identity == null || !principal.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            var userName = principal.FindFirst(ClaimTypes.Name)?.Value;

            var roles = principal.FindAll(ClaimTypes.Role);
            var hasPermission = roles.Any(p => RoleClaims.Contains(p.Value));

            if (userName.IsNotEmpty() && hasPermission)
            {
                return;
            }

            context.Result = new UnauthorizedResult();

        }

    }
}
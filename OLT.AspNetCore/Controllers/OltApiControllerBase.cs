using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    [ApiController]
    [Produces("application/json")]
    public abstract class OltApiControllerBase : ControllerBase
    {

        [NonAction]
        public virtual BadRequestObjectResult BadRequest(string error)
        {
            return BadRequest(new { message = error });
        }

        [NonAction]
        public OltInternalServerErrorObjectResult InternalServerError()
        {
            return new OltInternalServerErrorObjectResult();
        }
        [NonAction]
        public OltInternalServerErrorObjectResult InternalServerError(object value)
        {
            return new OltInternalServerErrorObjectResult(value);
        }
    }
}

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
            return BadRequest(new OltErrorHttp { Message = error });
        }

        [NonAction]
        public virtual OltInternalServerErrorObjectResult InternalServerError()
        {
            return new OltInternalServerErrorObjectResult();
        }
        [NonAction]
        public virtual OltInternalServerErrorObjectResult InternalServerError(object value)
        {
            return new OltInternalServerErrorObjectResult(value);
        }
    }
}

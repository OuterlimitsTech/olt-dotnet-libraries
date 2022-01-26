using Microsoft.AspNetCore.Mvc;
using OLT.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLT.Libraries.UnitTest.OLT.AspNetCore
{

    [Route("api/olt/tests")]
    public class OltTestController : OltApiControllerBase
    {
        [HttpGet, Route("simple")]
        public ActionResult GetSimple()
        {
            var result = new
            {
                id = 1,
            };

            return Ok(result);
        }

        [HttpGet, Route("throw-error")]
        public ActionResult TestInternalServerError(string value)
        {
            if (value == null)
            {
                return InternalServerError();
            }
            return InternalServerError(value);
        }


        [HttpGet, Route("bad-request")]
        public ActionResult TestBadRequest(string value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            return BadRequest(value);
        }
    }
}

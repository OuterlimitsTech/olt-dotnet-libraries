using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    public class OltInternalServerErrorObjectResult : ObjectResult
    {
        public OltInternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public OltInternalServerErrorObjectResult() : this(null)
        {
            
        }
    }
}

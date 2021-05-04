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

    }
}

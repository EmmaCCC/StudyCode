using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemo.Controllers
{
    [Route("v1/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class ApiControllerBaseV1: ControllerBase
    {

    }

    [Route("v2/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class ApiControllerBaseV2 : ControllerBase
    {

    }
}

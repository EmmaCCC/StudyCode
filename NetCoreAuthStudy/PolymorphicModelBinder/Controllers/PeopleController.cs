using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PolymorphicModelBinder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
     

        [HttpPost("save")]
        public IActionResult Save(People people1)
        {
            return Ok(people1);
        }
    }
}

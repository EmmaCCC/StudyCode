using Ids4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ids4Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly GwlDbContext gwlDb;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, GwlDbContext gwlDb)
        {
            _logger = logger;
            this.gwlDb = gwlDb;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("weather");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : Controller
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var afg11 = 981273;
            var fg11 = 981273;
            var ag11 = 981273;
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55)
            })
            .ToArray();
        }
    }
}

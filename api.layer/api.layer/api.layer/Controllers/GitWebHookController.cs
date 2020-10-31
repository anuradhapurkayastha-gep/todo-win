using api.layer.BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace api.layer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitWebHookController : Controller
    {
        private readonly ILogger<GitWebHookController> _logger;
        private readonly IGitActionsManager _gitActionsManager;

        public GitWebHookController(ILogger<GitWebHookController> logger, IGitActionsManager gitActionsManager)
        {
            _logger = logger;
            _gitActionsManager = gitActionsManager;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Post(GitActions gitActions)
        {
            if (gitActions.action == "opened")
            {
                _gitActionsManager.OpenRequestedCreated(gitActions);
            }
            else if (gitActions.action == "created")
            {
                _gitActionsManager.PullRequestedCreated(gitActions);
            }
            else if(gitActions.action == "review_requested")
            {
                return Ok(true);
            }

            return Ok(true);
        }
    }
}
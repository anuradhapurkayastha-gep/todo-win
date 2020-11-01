﻿using api.layer.BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO

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
            var abc = 19837;
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
            else if (gitActions.action == "completed")
            {
                _gitActionsManager.ChecksCompleted(gitActions);
            }
            else if(gitActions.action == "review_requested")
            { 
            }
            else if(gitActions.action == "submitted")
            {
                _gitActionsManager.PRReviewed(gitActions);
            }

            return Ok(true);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pambourg.Cleemy.Recruitement.Back.Senior.Models.Entities;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Controllers
{
    [ApiController]
    [Route(ApiPrefix + "/[controller]")]
    public class ExpenditureController : ControllerBase
    {
        private const string ApiPrefix = "api";


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ExpenditureController> _logger;
        private readonly IExpenditureService _expenditureService;

        public ExpenditureController(ILogger<ExpenditureController> logger, IExpenditureService expenditureService)
        {
            _logger = logger;
            _expenditureService = expenditureService;
        }

        [HttpGet]
        public string Get()
        {
            IEnumerable<Expenditure> expenditures = _expenditureService.GetExpenditureByUserId(1);
            return "hello";
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get2()
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
    }
}

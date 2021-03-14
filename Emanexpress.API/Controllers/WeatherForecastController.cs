using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emanexpress.API.Business.Email;
using Emanexpress.API.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Emanexpress.API.Controllers
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
        EmailDriverEmploymentApplicationHandler EmailDriverEmploymentApplicationHandler { get; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, EmailDriverEmploymentApplicationHandler emailDriverEmploymentApplicationHandler)
        {
            _logger = logger;
            EmailDriverEmploymentApplicationHandler = emailDriverEmploymentApplicationHandler;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var dd = new DtoDriverEmploymentApplication()
            //{
            //    DriverEmail = "adriantostega@hotmail.com"
            //};

            //EmailDriverEmploymentApplicationHandler.SendToAdministratorAsync(dd).Wait();
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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestWebApp2.Controllers
{
    /// <summary>
    ///     Контроллер для рбаоты с прогнозом погоды
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Получение прогноза погоды
        /// </summary>
        /// <param name="count">Кол-во записей</param>
        /// <returns>Прогноз погоды</returns>
        /// <response code="400">Если количество элементов меньше 1 или больше 10</response> 
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WeatherForecast>))]
        [HttpGet]
        public IActionResult Get(int count=5)
        {
            if (count < 1 || count > 10)
                return BadRequest("Count value should be between 1 and 10");

            var rng = new Random();
            return Ok(Enumerable.Range(1, count).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}

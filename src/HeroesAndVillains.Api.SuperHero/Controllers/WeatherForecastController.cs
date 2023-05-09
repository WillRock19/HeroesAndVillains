using Microsoft.AspNetCore.Mvc;

namespace HeroesAndVillains.Api.SuperHero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowersController : ControllerBase
    {
        private readonly ILogger<PowersController> _logger;

        public PowersController(ILogger<PowersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
            })
            .ToArray();
        }
    }
}
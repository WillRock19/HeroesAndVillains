using HeroesAndVillains.Api.SuperHero.DTO;
using HeroesAndVillains.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HeroesAndVillains.Api.SuperHero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HeroesController> _logger;
        private readonly HeroRepository _heroRepository;

        public HeroesController(ILogger<HeroesController> logger, HeroRepository heroRepository)
        {
            _logger = logger;
            _heroRepository = heroRepository;
        }

        [HttpGet("")]
        public IEnumerable<WeatherForecast> Get()
        {
            var allHeroes = _heroRepository.GetAll();


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
using FluentAssertions;
using HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs;

namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Specs
{
    [Collection(CollectionNames.GlobalTestCollection)]
    public class WeatherForecastController
    {
        private readonly GlobalContext _globalContext;

        public WeatherForecastController(GlobalContext globalContext)
        {
            _globalContext = globalContext;
        }

        [Fact]
        public void Test2()
        {
            var a = 10;
            a.Should().Be(10);
        }
    }
}

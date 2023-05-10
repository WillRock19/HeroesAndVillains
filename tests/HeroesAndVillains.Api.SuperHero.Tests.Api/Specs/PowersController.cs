using FluentAssertions;
using HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs;

namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Tests
{
    [Collection(CollectionNames.GlobalTestCollection)]
    public class PowersController
    {
        private readonly GlobalContext _globalContext;

        public PowersController(GlobalContext globalContext)
        {
            _globalContext = globalContext;
        }

        [Fact]
        public void Test1()
        {
            var a = 10;
            a.Should().Be(10);
        }
    }
}
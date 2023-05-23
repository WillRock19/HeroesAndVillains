using FluentAssertions;
using HeroesAndVillains.Domain.Models;

namespace HeroesAndVillains.Api.SuperHero.Tests.Unit.Models
{
    public class HeroTests
    {
        public class HasImage 
        {
            [Fact]
            public void WhenImageIdIsNotEmpty_ReturnsTrue()
            {
                var hero = new Hero() { ImageId = Guid.NewGuid() };
                hero.HasImage().Should().BeTrue();
            }

            [Fact]
            public void WhenImageIdIsEmpty_ReturnsFalse()
            {
                var hero = new Hero();
                hero.HasImage().Should().BeFalse();
            }
        }

        public class HasPowers 
        {
            [Fact]
            public void WhenPowersIsNotEmpty_ReturnsTrue()
            {
                var hero = new Hero() 
                { 
                    Powers = new List<Power> 
                    { 
                        new Power() { Name = "Test", Description = "Description" } 
                    } 
                };
                hero.HasPowers().Should().BeTrue();
            }

            [Fact]
            public void WhenPowersIsEmpty_ReturnsFalse()
            {
                var hero = new Hero();
                hero.HasPowers().Should().BeFalse();
            }
        }

        public class AllPowersAsString 
        {
            [Fact]
            public void WhenHeroHasPowers_ReturnsPowersNamesSeparatedByComma()
            {
                var powerName1 = "Invulnerability";
                var powerName2 = "Supersonic fly";

                var hero = new Hero()
                {
                    Powers = new List<Power>
                    {
                        new Power() { Name = powerName1, Description = "description 1" },
                        new Power() { Name = powerName2, Description = "description 2" },
                    }
                };
                hero.AllPowersAsString().Should().Be($"{powerName1}, {powerName2}");
            }

            [Fact]
            public void WhenHeroDontHavePowers_ReturnsEmptyString()
            {
                var hero = new Hero();
                hero.AllPowersAsString().Should().BeEmpty();
            }
        }
    }
}

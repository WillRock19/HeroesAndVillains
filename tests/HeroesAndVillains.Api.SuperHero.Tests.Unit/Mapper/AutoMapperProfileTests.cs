using AutoMapper;
using FluentAssertions;
using HeroesAndVillains.Api.SuperHero.DTO;
using HeroesAndVillains.Api.SuperHero.Mapper;
using HeroesAndVillains.Tests.Common.Builders.Models;

namespace HeroesAndVillains.Api.SuperHero.Tests.Unit.Mapper
{
    public class AutoMapperProfileTests
    {
        private readonly IMapper _mapper;

        public AutoMapperProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void WhenMappingHeroToHeroDTO_ShouldMapPropertiesCorrectly() 
        {
            var name = "Test Hero";
            var image = new byte[] { 1, 2, 3 };
            var id = Guid.NewGuid();

            var hero = new HeroBuilder()
                .WithId(id)
                .WithName(name)
                .WithRandomPower()
                .WithImage(image)
                .Build();

            var heroDTO = _mapper.Map<HeroDTO>(hero);

            heroDTO.Id.Should().Be(id);
            heroDTO.Name.Should().Be(hero.Name);
            heroDTO.Image.Should().BeEquivalentTo(image);
            heroDTO.PowersDescription.Should().Be(hero.AllPowersAsString());
        }
    }
}

using AutoMapper;
using HeroesAndVillains.Api.SuperHero.DTO;
using HeroesAndVillains.Domain.Models;

namespace HeroesAndVillains.Api.SuperHero.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Hero, HeroDTO>()
                .ForMember(dest => dest.PowersDescription, config => config.MapFrom(src => src.AllPowersAsString()));
        }
    }
}

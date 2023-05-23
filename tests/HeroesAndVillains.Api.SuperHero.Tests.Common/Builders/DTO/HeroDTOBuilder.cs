using HeroesAndVillains.Api.SuperHero.DTO;

namespace HeroesAndVillains.Tests.Common.Builders.DTO
{
    public class HeroDTOBuilder
    {
        public string? _powersDescription;
        public string? _name;
        public byte[]? _image;
        public Guid _id;

        public HeroDTOBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public HeroDTOBuilder WithPowersDescription(string description)
        {
            _powersDescription = description;
            return this;
        }

        public HeroDTOBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public HeroDTOBuilder WithImage(byte[] image)
        {
            _image = image;
            return this;
        }

        public HeroDTO Build() => new HeroDTO
        {
            Id = _id,
            Name = _name ?? string.Empty,
            PowersDescription = _powersDescription ?? string.Empty,
            Image = _image ?? new byte[] { },
        };
    }
}

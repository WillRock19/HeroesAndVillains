using HeroesAndVillains.Domain.Models;

namespace HeroesAndVillains.Tests.Common.Builders.Models
{
    public class HeroBuilder
    {
        public IEnumerable<Power>? _powers;
        public string? _name;
        public byte[]? _image;
        public Guid _imageId;
        public Guid _id;

        public HeroBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public HeroBuilder WithPowers(IEnumerable<Power> powers)
        {
            _powers = powers;
            return this;
        }

        public HeroBuilder WithRandomPower() 
        {
            var randomNumber = new Random().Next();
            _powers = new List<Power>() 
            { 
                new PowerBuilder().WithName($"Power {randomNumber}").WithDescription($"Description {randomNumber}").Build() 
            };

            return this;
        }

        public HeroBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public HeroBuilder WithImage(byte[] image)
        {
            _image = image;
            return this;
        }

        public HeroBuilder WithImageId(Guid imageId)
        {
            _imageId = imageId;
            return this;
        }

        public Hero Build() => new Hero
        {
            Id = _id,
            Name = _name ?? string.Empty,
            Powers = _powers ?? new List<Power>(),
            Image = _image ?? Array.Empty<byte>(),
            ImageId = _imageId
        };
    }
}

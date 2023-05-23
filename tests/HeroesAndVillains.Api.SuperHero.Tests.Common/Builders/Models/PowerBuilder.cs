using HeroesAndVillains.Domain.Models;

namespace HeroesAndVillains.Tests.Common.Builders.Models
{
    public class PowerBuilder
    {
        private string? _name;
        private string? _description;

        public PowerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PowerBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Power Build() => new Power
        {
            Name = _name ?? string.Empty,
            Description = _description ?? string.Empty
        };
    }
}

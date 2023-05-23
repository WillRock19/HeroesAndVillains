using HeroesAndVillains.Domain.Interfaces.Models;

namespace HeroesAndVillains.Domain.Models
{
    public class Power : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

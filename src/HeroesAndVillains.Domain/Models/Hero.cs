using HeroesAndVillains.Domain.Interfaces.Models;

namespace HeroesAndVillains.Domain.Models
{
    public class Hero : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Power> Powers { get; set; }
        public Guid ImageId { get; set; }
        public byte[]? Image { get; set; }

        public Hero()
        {
            Powers = new List<Power>();
        }

        public bool HasPowers() => Powers.Any();

        public bool HasImage() => ImageId != Guid.Empty;

        public string AllPowersAsString() => HasPowers() 
            ? string.Join(", ", Powers.Select(x => x.Name)) 
            : string.Empty;
    }
}

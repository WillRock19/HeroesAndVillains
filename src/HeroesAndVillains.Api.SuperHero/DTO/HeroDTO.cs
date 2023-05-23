namespace HeroesAndVillains.Api.SuperHero.DTO
{
    public class HeroDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PowersDescription { get; set; }
        public byte[] Image { get; set; }
    }
}

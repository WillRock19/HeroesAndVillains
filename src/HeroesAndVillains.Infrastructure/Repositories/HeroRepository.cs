using HeroesAndVillains.Domain.Models;

namespace HeroesAndVillains.Infrastructure.Repositories
{
    public class HeroRepository : MongoDbRepository<Hero>
    {
        public HeroRepository(string connectionString, string databaseName, string collectionName) 
            : base(connectionString, databaseName, collectionName) { }
    }
}

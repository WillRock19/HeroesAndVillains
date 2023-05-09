using MongoDB.Bson;
using MongoDB.Driver;

namespace HeroesAndVillains.Infrastructure.Repositories
{
    public abstract class MongoDbRepository<T>
    {
        protected readonly IMongoCollection<T> Collection;

        public MongoDbRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            Collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> Get(ObjectId id) 
        {
            var filterById = Builders<T>.Filter.Eq("_id", id);
            var result = await Collection.FindAsync(filterById);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(ObjectId id)
        {
            var result = await Collection.FindAsync(new BsonDocument());
            return await result.ToListAsync();
        }

        public async Task Save(T item) 
        {
            await Collection.InsertOneAsync(item);
        }

        public async Task SaveAll(IEnumerable<T> item)
        {
            await Collection.InsertManyAsync(item);
        }
    }
}

using FluentAssertions;
using HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs;
using HeroesAndVillains.Infrastructure.Repositories;
using MongoDB.Bson;

namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Specs.Repositories
{
    [Collection(CollectionNames.GlobalTestCollection)]
    public class MongoDbRepositoryTests
    {
        private const string _databaseName = "testsDatabase";
        private const string _collectionName = "entitiesCollection";
        private RepoForTests _repoForTests;

        public MongoDbRepositoryTests(GlobalContext globalContext) 
        {
            _repoForTests = new RepoForTests(globalContext.MongoDbTestConnectionString, _databaseName, _collectionName);
        }

        [Fact]
        public async Task Save_ShouldAddEntityToDatabase()
        {
            var entity = new EntityForTests() { Name = "Entity to save" };
            await _repoForTests.Save(entity);

            var allEntities = await _repoForTests.GetAll();

            entity.Id.Should().NotBe(ObjectId.Empty);
            allEntities.LastOrDefault().Should().BeEquivalentTo(entity);
        }

        [Fact]
        public async Task GetById_ShouldReturnTheExpectedEntity()
        {
            var entity = new EntityForTests() { Name = "Entity to retrieve by Id" };
            await _repoForTests.Save(entity);

            var savedEntity = await _repoForTests.GetById(entity.Id);
            savedEntity.Should().BeEquivalentTo(entity);
        }
    }

    internal class EntityForTests 
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }

    internal class RepoForTests : MongoDbRepository<EntityForTests> 
    {
        public RepoForTests(string connectionString, string databaseName, string collectionName)
            :base(connectionString, databaseName, collectionName) { }
    }
}

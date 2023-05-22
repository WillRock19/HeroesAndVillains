using Testcontainers.Azurite;
using Testcontainers.MongoDb;

namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs
{
    public class GlobalContext : IAsyncLifetime
    {
        public string MongoDbConnectionString { get; private set; }
        public string AzuriteConnectionString { get; private set; }

        private readonly MongoDbContainer _mongoDb;
        private readonly AzuriteContainer _azurite;


        public GlobalContext()
        {
            _mongoDb = new MongoDbBuilder()
                .WithImage("mongo:latest")
                .Build();

            _azurite = new AzuriteBuilder()
                .WithImage("mcr.microsoft.com/azure-storage/azurite")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _mongoDb.StartAsync();
            await _azurite.StartAsync();

            MongoDbConnectionString = _mongoDb.GetConnectionString();
            AzuriteConnectionString = _azurite.GetConnectionString();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            if (_mongoDb != null)
            {
                await _mongoDb.StopAsync();
                await _mongoDb.DisposeAsync();
            }

            if (_azurite != null)
            {
                await _azurite.StopAsync();
                await _azurite.DisposeAsync();
            }
        }
    }
}
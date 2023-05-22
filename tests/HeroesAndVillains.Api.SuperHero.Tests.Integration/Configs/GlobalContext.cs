using HeroesAndVillains.Tests.Common.Helpers;

namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs
{
    public class GlobalContext : IAsyncLifetime
    {
        public string MongoDbConnectionString => "mongodb://localhost:27017";    
        public string AzuriteConnectionString => "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        
        private DockerComposeManager _dockerManager;

        public GlobalContext()
        {
            var containerNames = new List<string>()
            {
                "mongo-db-for-tests",
                "azurite-for-tests"
            };
            var dockerComposePath = Path.Combine(Directory.GetCurrentDirectory(), "docker-compose.yml");

            _dockerManager = new DockerComposeManager(containerNames, dockerComposePath);
        }

        public async Task InitializeAsync()
        {
            await EnsureContainersAreUp();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _dockerManager.StopAndRemoveContainers();
            _dockerManager.Dispose();
        }

        private async Task EnsureContainersAreUp() 
        {
            var allRunning = await _dockerManager.GuaranteeContainersAreUp();

            if (!allRunning)
            {
                throw new ApplicationException("Not all obligatory containers are running; tests cannot be executed!");
            }
        }
    }
}

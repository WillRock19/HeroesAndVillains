namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs
{
    public class GlobalContext : IDisposable
    {
        public string MongoDbConnectionString => "mongodb://localhost:27017";
        public string AzuriteConnectionString => "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

        private DockerManager _dockerManager;

        public GlobalContext()
        {
            _dockerManager = new DockerManager();

            var containerNames = new List<string>()
            {
                "mongo-db-for-tests",
                "azurite-for-tests"
            };
            EnsureContainersAreUp(containerNames);
        }

        public void Dispose() 
        {
            _dockerManager.Dispose();
        }

        private void EnsureContainersAreUp(List<string> containerNames) 
        {
            var allRunning = _dockerManager
                .ContainersWithDatabasesAreUp(containerNames)
                .Result;

            if (!allRunning)
            {
                throw new ApplicationException("Not all obligatory containers are running; tests cannot be executed!");
            }
        }
    }
}

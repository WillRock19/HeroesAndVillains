namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs
{
    public class GlobalContext : IDisposable
    {
        public string MongoDbTestConnectionString;
        public string AzuriteConnectionString;

        private DockerManager _dockerManager;

        public GlobalContext()
        {
            MongoDbTestConnectionString = "mongodb://localhost:27017";
            AzuriteConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

            _dockerManager = new DockerManager();

            EnsureContainersAreUp();
        }

        public void Dispose() 
        {
            _dockerManager.Dispose();
        }

        private void EnsureContainersAreUp() 
        {
            var containersNames = new List<string>()
            {
                "mongo-db-for-tests",
                "azurite-for-tests"
            };

            var containersAreUp = _dockerManager
                .ContainersWithDatabasesAreUp(containersNames)
                .Result;

            if (!containersAreUp) 
            {
                throw new ApplicationException("Not all obligatory containers are running; tests cannot be executed!");
            }
        }
    }
}

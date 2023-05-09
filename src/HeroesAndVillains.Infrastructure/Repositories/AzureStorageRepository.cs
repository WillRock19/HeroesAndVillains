using Azure.Data.Tables;
using HeroesAndVillains.Domain.Interfaces.Repositories;

namespace HeroesAndVillains.Infrastructure.Repositories
{
    public class AzureStorageRepository : IAzureStorageRepository
    {
        private string _tableName;
        private TableClient _tableClient;

        public AzureStorageRepository(string nameOrConnectionString, string tableName) 
        {
            _tableName = tableName;
            _tableClient = new TableClient(nameOrConnectionString, tableName);

            CreateTableIfNotExist();
        }

        public void CreateTableIfNotExist() 
        {
            _tableClient.CreateIfNotExists();
        }
    }
}

using Azure.Data.Tables;
using HeroesAndVillains.Infrastructure.Interfaces;

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
        }

        public async Task<IAzureStorageRepository> CreateTableIfNotExist() 
        {
            await _tableClient.CreateIfNotExistsAsync();
            return this;
        }
    }
}

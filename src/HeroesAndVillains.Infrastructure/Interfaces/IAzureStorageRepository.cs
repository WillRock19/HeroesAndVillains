

namespace HeroesAndVillains.Infrastructure.Interfaces
{
    public interface IAzureStorageRepository
    {
        //Task<string> GetOne(string partition, string rowkey);
        //Task<string> GetAll(string partition, string rowkey);
        Task<IAzureStorageRepository> CreateTableIfNotExist();
    }
}

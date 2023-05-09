namespace HeroesAndVillains.Domain.Interfaces.Repositories
{
    public interface IAzureBlobStorage
    {
        Task SaveImageInBlobAsync(Guid recordId, string imageName, byte[] imageContent);

        Task<byte[]?> RetrieveImageFromBlobAsync(Guid recordId, string imageName);
    }
}

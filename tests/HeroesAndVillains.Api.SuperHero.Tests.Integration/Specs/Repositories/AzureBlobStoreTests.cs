using FluentAssertions;
using HeroesAndVillains.Api.SuperHero.Tests.Integration.Configs;
using HeroesAndVillains.Infrastructure.Repositories;
using HeroesAndVillains.Tests.Common.Resources.Helpers;

namespace HeroesAndVillains.Api.SuperHero.Tests.Integration.Specs.Repositories
{
    [Collection(CollectionNames.GlobalTestCollection)]
    public class AzureBlobStoreTests
    {
        private const string _containerName = "container-for-tests";
        private readonly AzureBlobStore _azureBlobStore;
        private readonly ImagesHelper _imageHelper;
        private readonly byte[] _defaultImage;

        public AzureBlobStoreTests(GlobalContext globalContext) 
        {
            _azureBlobStore = new AzureBlobStore(globalContext.AzuriteConnectionString, _containerName);
            _imageHelper = new ImagesHelper();
            _defaultImage = _imageHelper.Invincible();
        }

        public class SaveImageInBlobAsync : AzureBlobStoreTests 
        {
            public SaveImageInBlobAsync(GlobalContext globalContext) 
                : base(globalContext) { }

            [Fact]
            public async Task WhenSavingNewImageWithExistingNameAndId_ShouldReplaceExistingItem()
            {
                var defaultImageId = Guid.NewGuid();
                var defaultImageName = "invincible";
                await _azureBlobStore.SaveImageInBlobAsync(defaultImageId, defaultImageName, _defaultImage);

                var newImage = _imageHelper.RexSplode();
                await _azureBlobStore.SaveImageInBlobAsync(defaultImageId, defaultImageName, newImage);
                var imageSaved = await _azureBlobStore.RetrieveImageFromBlobAsync(defaultImageId, defaultImageName);

                imageSaved.Should()
                          .NotBeEquivalentTo(_defaultImage)
                          .And
                          .BeEquivalentTo(newImage);
            }
        }

        public class RetrieveImageFromBlobAsync : AzureBlobStoreTests
        {
            public RetrieveImageFromBlobAsync(GlobalContext globalContext)
                : base(globalContext) { }

            [Fact]
            public async Task WhenImageExists_ShouldReturnImage()
            {
                var imageId = Guid.NewGuid();
                var imageName = "invincible";
                await _azureBlobStore.SaveImageInBlobAsync(imageId, imageName, _defaultImage);

                var image = await _azureBlobStore.RetrieveImageFromBlobAsync(imageId, imageName);
                image.Should().NotBeNull().And.BeEquivalentTo(_defaultImage);
            }

            [Fact]
            public async Task WhenImageDontExists_ShouldReturnNull()
            {
                var image = await _azureBlobStore.RetrieveImageFromBlobAsync(Guid.NewGuid(), "non-existent-image");
                image.Should().BeNull();
            }
        } 
    }
}

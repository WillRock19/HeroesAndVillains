using HeroesAndVillains.Domain.Interfaces.Repositories;
using HeroesAndVillains.Infrastructure.Repositories;

namespace HeroesAndVillains.Api.SuperHero.Configs
{
    public static class DepedencyInjectionExtension
    {
        internal static void AddDepedencies(this IServiceCollection serviceCollection, ConnectionStrings connectionStrings) 
        {
            serviceCollection
                .AddScoped<IAzureBlobStorage>(provider =>
                {
                    return new AzureBlobStore(connectionStrings.AzureStorage, "Images");
                });
        }
    }
}

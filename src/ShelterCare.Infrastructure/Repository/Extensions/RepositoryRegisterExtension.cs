using Microsoft.Extensions.DependencyInjection;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Infrastructure.Repository.Extensions;

public static class RepositoryRegisterExtension
{
    public static void RegisterRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IShelterRepository, ShelterRepository>();
        serviceCollection.AddTransient<IAnimalSpecieRepository, AnimalSpecieRepository>();
        serviceCollection.AddTransient<IAnimalOwnerRepository, AnimalOwnerRepository>();
        serviceCollection.AddTransient<IAnimalRepository, AnimalRepository>();
    }
}

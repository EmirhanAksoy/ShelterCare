namespace ShelterCare.Core.Abstractions.Repository;

public interface IAnimalSpecieRepository : IRepository<AnimalSpecie> 
{
    Task<bool> CheckIfAnimalSpecieNameExists(string animalSpecieName);
}

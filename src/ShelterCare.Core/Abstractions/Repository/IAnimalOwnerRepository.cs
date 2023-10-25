namespace ShelterCare.Core.Abstractions.Repository;

public interface IAnimalOwnerRepository : IRepository<AnimalOwner>
{
    Task<bool> CheckIfAnimalOwnerExists(string nationalId);
}

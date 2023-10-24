using FluentValidation;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;
public class CreateAnimalSpecieCommandValidation : AbstractValidator<CreateAnimalSpecieCommand>
{
    private readonly IAnimalSpecieRepository _animalSpecieRepository;
    public CreateAnimalSpecieCommandValidation(IAnimalSpecieRepository animalSpecieRepository)
    {
        _animalSpecieRepository = animalSpecieRepository;

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MustAsync((x, _) => CheckIfAnimalSpecieNameExists(x)).WithErrorCode(AnimalSpecieNameAlreadyExists.Code).WithMessage(AnimalSpecieNameAlreadyExists.Message);
    }

    public async Task<bool> CheckIfAnimalSpecieNameExists(string animalSpecieName)
    {
        bool isExists = await _animalSpecieRepository.CheckIfAnimalSpecieNameExists(animalSpecieName);
        return !isExists;
    }
}

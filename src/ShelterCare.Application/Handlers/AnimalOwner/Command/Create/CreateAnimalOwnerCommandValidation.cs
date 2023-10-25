using FluentValidation;
using ShelterCare.Core.Abstractions.Repository;

namespace ShelterCare.Application;
public class CreateAnimalOwnerCommandValidation : AbstractValidator<CreateAnimalOwnerCommand>
{
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    public CreateAnimalOwnerCommandValidation(IAnimalOwnerRepository animalOwnerRepository)
    {
        _animalOwnerRepository = animalOwnerRepository;

        RuleFor(x => x.Fullname).NotEmpty();
        RuleFor(x => x.NationalId).NotEmpty();
        RuleFor(x => x.NationalId).MustAsync((x,_)=> NationalIdIsNotExists(x))
             .WithErrorCode(AnimalOwnerNationalIdAlreadyExists.Code)
             .WithMessage(AnimalOwnerNationalIdAlreadyExists.Message);
        RuleFor(x => x.NationalId).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
    public async Task<bool> NationalIdIsNotExists(string nationalId)
    {
        bool isExists = await _animalOwnerRepository.CheckIfAnimalOwnerExists(nationalId);
        return !isExists;
    }
}
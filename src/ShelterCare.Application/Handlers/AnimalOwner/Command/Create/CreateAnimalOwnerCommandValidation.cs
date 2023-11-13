using FluentValidation;

namespace ShelterCare.Application;
public class CreateAnimalOwnerCommandValidation : AbstractValidator<CreateAnimalOwnerCommand>
{
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    public CreateAnimalOwnerCommandValidation(IAnimalOwnerRepository animalOwnerRepository)
    {
        _animalOwnerRepository = animalOwnerRepository;

        RuleFor(x => x.Fullname).NotEmpty();
        RuleFor(x => x.NationalId).NotEmpty();
        RuleFor(x => x.NationalId).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
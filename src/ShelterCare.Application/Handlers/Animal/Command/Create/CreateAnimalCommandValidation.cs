using FluentValidation;
namespace ShelterCare.Application;
public class CreateAnimalCommandValidation : AbstractValidator<CreateAnimalCommand>
{
    public CreateAnimalCommandValidation()
    {
        RuleFor(x => x.ShelterId).NotEmpty();
        RuleFor(x => x.AnimalSpecieId).NotEmpty();
        RuleFor(x => x.OwnerId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.UniqueIdentifier).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.JoiningDate).NotEmpty();
    }
}
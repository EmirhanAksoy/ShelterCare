using FluentValidation;
namespace ShelterCare.Application;
public class UpdateAnimalCommandValidation : AbstractValidator<UpdateAnimalCommand>
{
    public UpdateAnimalCommandValidation()
    {
        RuleFor(x => x.ShelterId).NotEmpty();
        RuleFor(x => x.AnimalSpecieId).NotEmpty();
        RuleFor(x => x.OwnerId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.UniqueIdentifier).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.JoiningDate).NotEmpty();
        RuleFor(x => x.IsNeutered).NotNull();
        RuleFor(x => x.IsDisabled).NotNull();
    }
}
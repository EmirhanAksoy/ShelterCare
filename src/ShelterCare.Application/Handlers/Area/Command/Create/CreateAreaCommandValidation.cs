using FluentValidation;
namespace ShelterCare.Application;
public class CreateAreaCommandValidation : AbstractValidator<CreateAreaCommand>
{
    public CreateAreaCommandValidation()
    {
        RuleFor(x => x.ShelterId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.AreaInSquareMeters).NotEmpty();
    }
}
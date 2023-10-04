using FluentValidation;

namespace ShelterCare.Application;

public class CreateShelterCommandValidation : AbstractValidator<CreateShelterCommand>
{
    public CreateShelterCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.OwnerFullName).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.FoundationDate).NotEmpty();
        RuleFor(x => x.TotalAreaInSquareMeters).NotEmpty();
    }
}

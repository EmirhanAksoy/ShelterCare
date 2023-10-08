using FluentValidation;
namespace ShelterCare.Application;
public class UpdateShelterCommandValidation : AbstractValidator<UpdateShelterCommand>
{
    public UpdateShelterCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.OwnerFullName).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.FoundationDate).NotEmpty();
        RuleFor(x => x.TotalAreaInSquareMeters).NotEmpty();
    }
}

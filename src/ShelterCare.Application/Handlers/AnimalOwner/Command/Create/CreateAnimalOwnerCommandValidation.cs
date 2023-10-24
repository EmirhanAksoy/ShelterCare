using FluentValidation;
namespace ShelterCare.Application;
public class CreateAnimalOwnerCommandValidation : AbstractValidator<CreateAnimalOwnerCommand>
{
    public CreateAnimalOwnerCommandValidation()
    {
        RuleFor(x => x.Fullname).NotEmpty();
        RuleFor(x => x.NationalId).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
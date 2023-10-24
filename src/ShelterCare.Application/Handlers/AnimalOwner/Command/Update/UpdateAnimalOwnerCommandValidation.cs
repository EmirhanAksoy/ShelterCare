using FluentValidation;
namespace ShelterCare.Application;
public class UpdateAnimalOwnerCommandValidation : AbstractValidator<UpdateAnimalOwnerCommand>
{
    public UpdateAnimalOwnerCommandValidation()
    {
        RuleFor(x => x.Fullname).NotEmpty();
        RuleFor(x => x.NationalId).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
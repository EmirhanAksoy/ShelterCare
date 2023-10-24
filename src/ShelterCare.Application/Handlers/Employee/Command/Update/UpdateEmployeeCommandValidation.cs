using FluentValidation;
namespace ShelterCare.Application;
public class UpdateEmployeeCommandValidation : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidation()
    {
        RuleFor(x => x.ShelterId).NotEmpty();
        RuleFor(x => x.Fullname).NotEmpty();
        RuleFor(x => x.NationalId).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.IsOwner).NotEmpty();
        RuleFor(x => x.DateOfStart).NotEmpty();
        RuleFor(x => x.DismissalDate).NotEmpty();
    }
}
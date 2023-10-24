using FluentValidation;
namespace ShelterCare.Application;
public class DeleteEmployeeByIdCommandValidation : AbstractValidator<DeleteEmployeeByIdCommand>
{
    public DeleteEmployeeByIdCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
using FluentValidation;
namespace ShelterCare.Application;
public class DeleteAreaByIdCommandValidation : AbstractValidator<DeleteAreaByIdCommand>
{
    public DeleteAreaByIdCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
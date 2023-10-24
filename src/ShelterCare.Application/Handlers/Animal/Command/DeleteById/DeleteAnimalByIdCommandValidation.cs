using FluentValidation;
namespace ShelterCare.Application;
public class DeleteAnimalByIdCommandValidation : AbstractValidator<DeleteAnimalByIdCommand>
{
    public DeleteAnimalByIdCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
using FluentValidation;
namespace ShelterCare.Application;

public class DeleteAnimalSpecieByIdCommandValidation : AbstractValidator<DeleteAnimalSpecieByIdCommand>
{
    public DeleteAnimalSpecieByIdCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

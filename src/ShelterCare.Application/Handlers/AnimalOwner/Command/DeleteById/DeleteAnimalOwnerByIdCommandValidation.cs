using FluentValidation;
namespace ShelterCare.Application;
public class DeleteAnimalOwnerByIdCommandValidation : AbstractValidator<DeleteAnimalOwnerByIdCommand>
{
    public DeleteAnimalOwnerByIdCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
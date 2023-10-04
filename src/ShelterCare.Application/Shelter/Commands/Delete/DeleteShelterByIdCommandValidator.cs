using FluentValidation;

namespace ShelterCare.Application;

public class DeleteShelterByIdCommandValidator : AbstractValidator<DeleteShelterByIdCommand>
{
    public DeleteShelterByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}

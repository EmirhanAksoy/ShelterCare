using FluentValidation;

namespace ShelterCare.Application;

public class GetShelterByIdQueryValidator : AbstractValidator<GetShelterByIdQuery>
{
    public GetShelterByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}

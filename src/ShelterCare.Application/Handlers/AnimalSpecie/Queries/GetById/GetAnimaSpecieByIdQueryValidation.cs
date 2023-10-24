using FluentValidation;

namespace ShelterCare.Application;

public class GetAnimaSpecieByIdQueryValidation : AbstractValidator<GetAnimaSpecieByIdQuery>
{
    public GetAnimaSpecieByIdQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

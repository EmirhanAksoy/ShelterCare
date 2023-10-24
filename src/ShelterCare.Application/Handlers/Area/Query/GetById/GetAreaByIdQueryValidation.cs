using FluentValidation;
namespace ShelterCare.Application;
public class GetAreaByIdQueryValidation : AbstractValidator<GetAreaByIdQuery>
{
    public GetAreaByIdQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
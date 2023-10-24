using FluentValidation;
namespace ShelterCare.Application;
public class GetAnimalByIdQueryValidation : AbstractValidator<GetAnimalByIdQuery>
{
    public GetAnimalByIdQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
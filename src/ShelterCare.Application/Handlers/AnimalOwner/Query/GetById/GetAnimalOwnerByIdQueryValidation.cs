using FluentValidation;
namespace ShelterCare.Application;
public class GetAnimalOwnerByIdQueryValidation : AbstractValidator<GetAnimalOwnerByIdQuery>
{
    public GetAnimalOwnerByIdQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
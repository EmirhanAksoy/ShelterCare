using FluentValidation;
namespace ShelterCare.Application;
public class GetEmployeeByIdQueryValidation : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
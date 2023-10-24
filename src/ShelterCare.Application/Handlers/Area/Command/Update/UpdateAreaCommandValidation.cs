using FluentValidation;
namespace ShelterCare.Application;
public class UpdateAreaCommandValidation : AbstractValidator<UpdateAreaCommand>
{
    public UpdateAreaCommandValidation()
    {
        RuleFor(x => x.ShelterId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.AreaInSquareMeters).NotEmpty();

    }
}
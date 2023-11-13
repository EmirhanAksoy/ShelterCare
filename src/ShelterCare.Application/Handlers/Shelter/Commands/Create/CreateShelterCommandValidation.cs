using FluentValidation;

namespace ShelterCare.Application;

public class CreateShelterCommandValidation : AbstractValidator<CreateShelterCommand>
{
    private readonly IShelterRepository _shelterRepository;
    public CreateShelterCommandValidation(IShelterRepository shelterRepository)
    {
        _shelterRepository = shelterRepository;

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MustAsync((x, _) => CheckIfShelterNameExists(x)).WithErrorCode(ShelterNameAlreadyExists.Code).WithMessage(ShelterNameAlreadyExists.Message);
        RuleFor(x => x.OwnerFullName).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.FoundationDate).NotEmpty();
        RuleFor(x => x.TotalAreaInSquareMeters).NotEmpty();
    }

    public async Task<bool> CheckIfShelterNameExists(string shelterName)
    {
        bool isExists = await _shelterRepository.CheckIfShelterNameExists(shelterName);
        return !isExists;
    }
}

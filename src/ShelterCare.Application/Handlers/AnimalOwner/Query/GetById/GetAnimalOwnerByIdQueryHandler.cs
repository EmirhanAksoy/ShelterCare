namespace ShelterCare.Application;

public class GetAnimalOwnerByIdQueryHandler : IRequestHandler<GetAnimalOwnerByIdQuery, Response<AnimalOwner>>
{
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    private readonly ILogger<GetAnimalOwnerByIdQueryHandler> _logger;
    public GetAnimalOwnerByIdQueryHandler(IAnimalOwnerRepository animalOwnerRepository, ILogger<GetAnimalOwnerByIdQueryHandler> logger)
    {
        _animalOwnerRepository = animalOwnerRepository;
        _logger = logger;
    }
    public async Task<Response<AnimalOwner>> Handle(GetAnimalOwnerByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            GetAnimalOwnerByIdQueryValidation validationRules = new();
            ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages} {animalOwnerId}", ValidationError.Code, ValidationError.Message, errorMessages, request.Id.ToString());
                return Response<AnimalOwner>.ErrorResult(ValidationError.Code, errorMessages);
            }
            AnimalOwner animalOwner = await _animalOwnerRepository.Get(request.Id);
            if (animalOwner is null)
            {
                _logger.LogError(AnimalOwnerNotFound.EventId, "{Code} {Message}  {animalOwnerId}", AnimalOwnerNotFound.Code, AnimalOwnerNotFound.Message, request.Id.ToString());
                return Response<AnimalOwner>.ErrorResult(AnimalOwnerNotFound.Code, AnimalOwnerNotFound.Message);
            }
            return Response<AnimalOwner>.SuccessResult(animalOwner);
        }
        catch (Exception exception)
        {
            _logger.LogError(GetAnimalOwnerByIdQueryFailed.EventId, exception, "{Code} {Message} {animalOwnerId}", GetAnimalOwnerByIdQueryFailed.Code, GetAnimalOwnerByIdQueryFailed.Message, request.Id.ToString());
            return Response<AnimalOwner>.ErrorResult(GetAnimalOwnerByIdQueryFailed.Code, GetAnimalOwnerByIdQueryFailed.Message);
        };
    }
}

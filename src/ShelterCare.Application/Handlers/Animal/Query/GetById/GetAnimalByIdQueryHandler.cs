
namespace ShelterCare.Application;

public class GetAnimalByIdQueryHandler : IRequestHandler<GetAnimalByIdQuery,Response<Animal>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly ILogger<GetAnimalByIdQueryHandler> _logger;
    public GetAnimalByIdQueryHandler(IAnimalRepository animalRepository, ILogger<GetAnimalByIdQueryHandler> logger)
    {
        _animalRepository = animalRepository;
        _logger = logger;
    }
    public async Task<Response<Animal>> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            GetAnimalByIdQueryValidation validationRules = new();
            ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages} {animalId}", ValidationError.Code, ValidationError.Message, errorMessages, request.Id.ToString());
                return Response<Animal>.ErrorResult(ValidationError.Code, errorMessages);
            }
            Animal animal = await _animalRepository.Get(request.Id);
            if (animal is null)
            {
                _logger.LogError(AnimalNotFound.EventId, "{Code} {Message}  {animalId}", AnimalNotFound.Code, AnimalNotFound.Message, request.Id.ToString());
                return Response<Animal>.ErrorResult(AnimalNotFound.Code, AnimalNotFound.Message);
            }
            return Response<Animal>.SuccessResult(animal);
        }
        catch (Exception exception)
        {
            _logger.LogError(GetAnimalByIdQueryFailed.EventId, exception, "{Code} {Message} {animalId}", GetAnimalByIdQueryFailed.Code, GetAnimalByIdQueryFailed.Message, request.Id.ToString());
            return Response<Animal>.ErrorResult(GetAnimalByIdQueryFailed.Code, GetAnimalByIdQueryFailed.Message);
        };
    }
}

using ShelterCare.Infrastructure.ExternalAPIs;
namespace ShelterCare.Application;
public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, Response<Animal>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CreateAnimalCommandHandler> _logger;
    public CreateAnimalCommandHandler(IAnimalRepository animalRepository, ILogger<CreateAnimalCommandHandler> logger, IHttpClientFactory httpClientFactory)
    {
        _animalRepository = animalRepository;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }


    public async Task<Response<Animal>> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CreateAnimalCommandValidation validator = new();
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError("{Code} {Message} : {@errorMessages}", ValidationError.Code, ValidationError.Message, errorMessages);
                return Response<Animal>.ErrorResult(ValidationError.Code, ValidationError.Message);
            }

            ConfirmApiHandler confirmApiHandler = new(_httpClientFactory);
            bool ownerConfirmation = await confirmApiHandler.ConfirmOwner(request.OwnerId);
            if (!ownerConfirmation)
            {
                _logger.LogError("{Code} {Message} : {@animal}", AnimalOwnerConfirmationFailed.Code, AnimalOwnerConfirmationFailed.Message, request);
                return Response<Animal>.ErrorResult(AnimalOwnerConfirmationFailed.Code, AnimalOwnerConfirmationFailed.Message);
            }
            bool animaConfirmation = await confirmApiHandler.ConfirmAnimal(request.UniqueIdentifier);
            if (!animaConfirmation)
            {
                _logger.LogError("{Code} {Message} : {@animal}", AnimalConfirmationFailed.Code, AnimalConfirmationFailed.Message, request);
                return Response<Animal>.ErrorResult(CreateAnimalCommandFailed.Code, CreateAnimalCommandFailed.Message);
            }
            bool ownerOfAnimalConfirmation = await confirmApiHandler.ConfirmAnimalOwner(request.OwnerId, request.UniqueIdentifier);
            if (!ownerOfAnimalConfirmation)
            {
                _logger.LogError("{Code} {Message} : {@animal}", OwnerOfAnimalConfirmationFailed.Code, OwnerOfAnimalConfirmationFailed.Message, request);
                return Response<Animal>.ErrorResult(OwnerOfAnimalConfirmationFailed.Code, OwnerOfAnimalConfirmationFailed.Message);
            }

            Animal createdAnimal = await _animalRepository.Create(new Animal()
            {
                OwnerId = request.OwnerId,
                AnimalSpecieId = request.AnimalSpecieId,
                IsDisabled = request.IsDisabled,
                IsNeutered = request.IsNeutered,
                JoiningDate = request.JoiningDate,
                UniqueIdentifier = request.UniqueIdentifier,
                DateOfBirth = request.DateOfBirth,
                ShelterId = request.ShelterId,
                Name = request.Name,
            });

            _logger.LogInformation("Animal created successfully {@animal}", createdAnimal);
            return Response<Animal>.SuccessResult(createdAnimal);
        }
        catch (Exception exception)
        {
            _logger.LogError(CreateAnimalCommandFailed.EventId, exception, "{Code} {Message} {@animal}", CreateAnimalCommandFailed.Code, CreateAnimalCommandFailed.Message, request);
            return Response<Animal>.ErrorResult(CreateAnimalCommandFailed.Code, CreateAnimalCommandFailed.Message);
        }
    }
}

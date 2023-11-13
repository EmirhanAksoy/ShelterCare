using ShelterCare.Infrastructure.ExternalAPIs;
namespace ShelterCare.Application;

public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand, Response<Animal>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<UpdateAnimalCommandHandler> _logger;
    public UpdateAnimalCommandHandler(IAnimalRepository animalRepository, ILogger<UpdateAnimalCommandHandler> logger, IHttpClientFactory httpClientFactory)
    {
        _animalRepository = animalRepository;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }


    public async Task<Response<Animal>> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            UpdateAnimalCommandValidation validator = new();
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

            Animal updatedAnimal = await _animalRepository.Update(new Animal()
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

            _logger.LogInformation("Animal updated successfully {@animal}", updatedAnimal);
            return Response<Animal>.SuccessResult(updatedAnimal);
        }
        catch (Exception exception)
        {
            _logger.LogError(UpdateAnimalCommandFailed.EventId, exception, "{Code} {Message} {@animal}", UpdateAnimalCommandFailed.Code, UpdateAnimalCommandFailed.Message, request);
            return Response<Animal>.ErrorResult(UpdateAnimalCommandFailed.Code, UpdateAnimalCommandFailed.Message);
        }
    }
}
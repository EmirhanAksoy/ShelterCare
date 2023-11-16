using ShelterCare.Infrastructure.ExternalAPIs;
namespace ShelterCare.Application;

public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand, Response<Animal>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalSpecieRepository _animalSpecieRepository;
    private readonly IShelterRepository _shelterRepository;
    private readonly IAnimalOwnerRepository _animalOwnerRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<UpdateAnimalCommandHandler> _logger;
    public UpdateAnimalCommandHandler(IAnimalRepository animalRepository, ILogger<UpdateAnimalCommandHandler> logger, IHttpClientFactory httpClientFactory, IAnimalSpecieRepository animalSpecieRepository, IShelterRepository shelterRepository, IAnimalOwnerRepository animalOwnerRepository)
    {
        _animalRepository = animalRepository;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _animalSpecieRepository = animalSpecieRepository;
        _shelterRepository = shelterRepository;
        _animalOwnerRepository = animalOwnerRepository;
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
                return Response<Animal>.ErrorResult(ValidationError.Code, errorMessages);
            }

            Shelter shelter = await _shelterRepository.Get(request.ShelterId);
            if (shelter is null)
            {
                _logger.LogError(ShelterNotFound.EventId, "{Code} {Message}  {shelterId}", ShelterNotFound.Code, ShelterNotFound.Message, request.ShelterId.ToString());
                return Response<Animal>.ErrorResult(ShelterNotFound.Code, ShelterNotFound.Message);
            }
            AnimalSpecie animalSpecie = await _animalSpecieRepository.Get(request.AnimalSpecieId);
            if (animalSpecie is null)
            {
                _logger.LogError(ShelterNotFound.EventId, "{Code} {Message} {animalSpecieId}", AnimalSpecieNotFound.Code, AnimalSpecieNotFound.Message, request.AnimalSpecieId.ToString());
                return Response<Animal>.ErrorResult(AnimalSpecieNotFound.Code, AnimalSpecieNotFound.Message);
            }
            AnimalOwner animalOwner = await _animalOwnerRepository.Get(request.OwnerId);
            if (animalOwner is null)
            {
                _logger.LogError(AnimalOwnerNotFound.EventId, "{Code} {Message}  {animalOwnerId}", AnimalOwnerNotFound.Code, AnimalOwnerNotFound.Message, request.OwnerId.ToString());
                return Response<Animal>.ErrorResult(AnimalOwnerNotFound.Code, AnimalOwnerNotFound.Message);
            }
            ConfirmApiHandler confirmApiHandler = new(_httpClientFactory);
            bool animaConfirmation = await confirmApiHandler.ConfirmAnimal(request.UniqueIdentifier);
            if (!animaConfirmation)
            {
                _logger.LogError("{Code} {Message} : {@animal}", AnimalConfirmationFailed.Code, AnimalConfirmationFailed.Message, request);
                return Response<Animal>.ErrorResult(AnimalConfirmationFailed.Code, AnimalConfirmationFailed.Message);
            }
            bool ownerOfAnimalConfirmation = await confirmApiHandler.ConfirmAnimalOwner(animalOwner.NationalId, request.UniqueIdentifier);
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
                Id = request.Id
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
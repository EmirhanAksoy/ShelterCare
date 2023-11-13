namespace ShelterCare.Application;

public class DeleteAnimalByIdCommandHandler : IRequestHandler<DeleteAnimalByIdCommand, Response<bool>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly ILogger<DeleteAnimalByIdCommandHandler> _logger;
    public DeleteAnimalByIdCommandHandler(IAnimalRepository animalRepository,ILogger<DeleteAnimalByIdCommandHandler> logger)
    {
        _animalRepository = animalRepository;
        _logger = logger;
    }
    public async Task<Response<bool>> Handle(DeleteAnimalByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            DeleteAnimalByIdCommandValidation validationRules = new();
            ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages} {animalId}", ValidationError.Code, ValidationError.Message, errorMessages, request.Id.ToString());
                return Response<bool>.ErrorResult(ValidationError.Code, errorMessages);
            }
            Animal animal = await _animalRepository.Get(request.Id);
            if (animal is null)
            {
                _logger.LogError(AnimalNotFound.EventId, "{Code} {Message}  {animalId}", AnimalNotFound.Code, AnimalNotFound.Message, request.Id.ToString());
                return Response<bool>.ErrorResult(AnimalNotFound.Code, AnimalNotFound.Message);
            }
            bool isDeleted = await _animalRepository.Delete(request.Id);
            if (isDeleted)
            {
                _logger.LogInformation("Animal deleted successfully {animalId}", request.Id.ToString());
            }
            return Response<bool>.SuccessResult(isDeleted);
        }
        catch (Exception exception)
        {
            _logger.LogError(DeleteAnimalByIdCommandFailed.EventId, exception, "{Code} {Message} {animalId}", DeleteAnimalByIdCommandFailed.Code, DeleteAnimalByIdCommandFailed.Message, request.Id.ToString());
            return Response<bool>.ErrorResult(DeleteAnimalByIdCommandFailed.Code, DeleteAnimalByIdCommandFailed.Message);
        }
    }
}

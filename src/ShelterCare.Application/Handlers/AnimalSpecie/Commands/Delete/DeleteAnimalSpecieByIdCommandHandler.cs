namespace ShelterCare.Application;

public class DeleteAnimalSpecieByIdCommandHandler : IRequestHandler<DeleteAnimalSpecieByIdCommand, Response<bool>>
{
    private readonly IAnimalSpecieRepository _animalSpecieRepository;
    private readonly ILogger<DeleteAnimalSpecieByIdCommandHandler> _logger;

    public DeleteAnimalSpecieByIdCommandHandler(IAnimalSpecieRepository animalSpecieRepository, ILogger<DeleteAnimalSpecieByIdCommandHandler> logger)
    {
        _animalSpecieRepository = animalSpecieRepository;
        _logger = logger;
    }
    public async Task<Response<bool>> Handle(DeleteAnimalSpecieByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            DeleteAnimalSpecieByIdCommandValidation validationRules = new();
            ValidationResult validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages} {animaSpecieId}", ValidationError.Code, ValidationError.Message, errorMessages, request.Id.ToString());
                return Response<bool>.ErrorResult(ValidationError.Code, errorMessages);
            }
            AnimalSpecie animalSpecie = await _animalSpecieRepository.Get(request.Id);
            if (animalSpecie is null)
            {
                _logger.LogError(AnimalSpecieNotFound.EventId, "{Code} {Message} {animaSpecieId}", AnimalSpecieNotFound.Code, AnimalSpecieNotFound.Message, request.Id.ToString());
                return Response<bool>.ErrorResult(AnimalSpecieNotFound.Code, AnimalSpecieNotFound.Message);
            }
            bool isDeleted = await _animalSpecieRepository.Delete(request.Id);
            if (isDeleted)
            {
                _logger.LogInformation("Animal specie deleted successfully {animaSpecieId}", request.Id.ToString());
            }
            return Response<bool>.SuccessResult(isDeleted);
        }
        catch (Exception exception)
        {
            _logger.LogError(DeleteAnimalSpecieByIdCommandFailed.EventId, exception, "{Code} {Message} {animaSpecieId}", DeleteAnimalSpecieByIdCommandFailed.Code, DeleteAnimalSpecieByIdCommandFailed.Message, request.Id.ToString());
            return Response<bool>.ErrorResult(DeleteAnimalSpecieByIdCommandFailed.Code, DeleteAnimalSpecieByIdCommandFailed.Message);
        }
    }
}

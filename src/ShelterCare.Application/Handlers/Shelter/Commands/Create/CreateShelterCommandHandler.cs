namespace ShelterCare.Application;

public class CreateShelterCommandHandler : IRequestHandler<CreateShelterCommand, Response<Shelter>>
{

    private readonly IShelterRepository _shelterRepository;
    private readonly ILogger<CreateShelterCommandHandler> _logger;

    public CreateShelterCommandHandler(IShelterRepository shelterRepository, ILogger<CreateShelterCommandHandler> logger)
    {
        _shelterRepository = shelterRepository;
        _logger = logger;
    }


    public async Task<Response<Shelter>> Handle(CreateShelterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CreateShelterCommandValidation validationRules = new(_shelterRepository);
            var validationResult = await validationRules.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.ConvertAll(x => x.ErrorMessage);
                _logger.LogError(ValidationError.EventId, "{Code} {Message} : {@errorMessages}", ValidationError.Code, ValidationError.Message, errorMessages);
                return Response<Shelter>.ErrorResult(ValidationError.Code, errorMessages);
            }
            Shelter shelter = new()
            {
                Name = request.Name,
                Address = request.Address,
                OwnerFullName = request.OwnerFullName,
                FoundationDate = request.FoundationDate,
                TotalAreaInSquareMeters = request.TotalAreaInSquareMeters,
                Website = request.Website
            };
            Shelter createdShelter = await _shelterRepository.Create(shelter);
            _logger.LogInformation("Shelter created successfully {@shelter}", createdShelter);
            return Response<Shelter>.SuccessResult(createdShelter);
        }
        catch (Exception exception)
        {
            _logger.LogError(CreateShelterCommandFailed.EventId, exception, "{Code} {Message} {@shelter}", CreateShelterCommandFailed.Code, CreateShelterCommandFailed.Message, request);
            return Response<Shelter>.ErrorResult(CreateShelterCommandFailed.Code, CreateShelterCommandFailed.Message);
        }
    }
}
